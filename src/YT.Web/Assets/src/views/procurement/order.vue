<template>
    <div class="animated fadeIn">
        <Row>
            <milk-table ref="list" :layout="[22,2]" :columns="cols" :search-api="searchApi" :params="params">
                <template slot="search">
                    <Form ref="params" :model="params" inline :label-width="60">
                        <FormItem label="联系人">
                            <Input v-model="params.name" placeholder="请输入用户姓名"></Input>
                        </FormItem>
                        <FormItem label="手机号码">
                            <Input v-model="params.mobile" placeholder="请输入手机号码"></Input>
                        </FormItem>
                        <FormItem label="开始时间">
                            <DatePicker type="date" :editable="false" v-model="params.start" placeholder="开始时间" style="width: 140px"></DatePicker>
                        </FormItem>
                        <FormItem label="截至时间">
                            <DatePicker type="date" :editable="false" v-model="params.end" placeholder="截至时间" style="width: 140px"></DatePicker>
                        </FormItem>
                    </Form>
                </template>
            </milk-table>
        </Row>
    </div>
</template>

<script>
import { orders, order, completeorder } from 'api/products';
export default {
    name: 'account',
    data() {
        return {
            cols: [
                {
                    title: '订单号',
                    key: 'orderNum'
                },
                  {
                    title: '商品名',
                    key: 'productName'
                },
                {
                    title: '价格',
                    key: 'totalPrice'
                },
                {
                    title: '公司名称',
                    key: 'companyName'
                },
                {
                    title: '联系人',
                    key: 'contact',
                },
                {
                    title: '手机号',
                    key: 'mobile',
                },
                {
                    title: '创建时间',
                    key: 'creationTime',
                    render: (h, params) => {
                        return this.$fmtTime(params.row.creationTime);
                    }
                },
                {
                    title: '状态',
                    key: 'isActive',
                    render: (h, params) => {
                        return params.row.state != null ? (params.row.state != null && params.row.state ? '已完成' : '已取消') : '未完成';
                    }
                },
                {
                    title: '操作',
                    key: 'action',
                    width:'130px',
                    align: 'center',
                    render: (h, params) => {
                        const childs = [];
                        childs.push(h('Button', {
                            props: {
                                type: 'primary',
                                size: 'small'
                            },
                            style: {
                                marginRight: '5px'
                            },
                            on: {
                                click: () => {
                                    this.detail(params.row)
                                }
                            }
                        }, '查看'));
                        if (!params.row || !params.row.state) {
                            childs.push(h('Button', {
                                props: {
                                    type: 'error',
                                    size: 'small'
                                },
                                on: {
                                    click: () => {
                                        this.complete(params.row)
                                    }
                                }
                            }, '完成'));
                        }
                        return h('div', childs);
                    }
                }
            ],
            searchApi: orders,
            params: { name: '', mobile: '', start: null, end: null },
            modal: {
                isShow: false, title: '订单详情', current: null
            }
        }
    },
    created() {
        var self = this;
        self.$root.eventHub.$on('order', () => {
            self.cancel();
        });
    },
    destroyed() {
        this.$root.eventHub.$off('order');
    },
    methods: {
        // 完成
        complete(model) {
            var table = this.$refs.list;
            this.$Modal.confirm({
                title: '操作提示', content: "确定要完成当前订单么?",
                onOk: () => {
                    const parms = { id: model.id }
                    completeorder(parms).then(c => {
                        if (c.data.success) {
                            table.initData();
                        }
                    }).catch(e => {
                        this.$Message.error(e.response.data.error.message);
                    })
                }
            })
        },
        detail(row) {
            this.$router.push({ path: '/order', query: { id: row.id } })
        },
        save() {
            this.$refs.account.commit();
        },
        cancel() {
            this.modal.isShow = false;
            this.modal.current = null;
            this.$refs.list.initData();
        }
    },
    mounted() {
    }
}
</script>


<style type="text/css" scoped>

</style>