<template>
    <div class="animated fadeIn">
        <Row>
            <milk-table :layout="[18,4,2]" ref="list" :columns="cols" :search-api="searchApi" :params="params">
                <template slot="search">
                    <Form ref="params" :model="params" inline :label-width="60">
                        <FormItem label="客户昵称">
                            <Input v-model="params.name" placeholder="客户昵称"></Input>
                        </FormItem>
                        <FormItem label="客户手机">
                            <Input v-model="params.phone" placeholder="客户手机"></Input>
                        </FormItem>
                        <FormItem label="推广专员">
                            <Input v-model="params.promoterName" placeholder="推广专员"></Input>
                        </FormItem>
                    </Form>
                </template>
            
            </milk-table>
        </Row>
        <!-- 添加和编辑窗口 -->
        <Modal :width="1000" :transfer="false" v-model="modal.isEdit" :title="modal.title" :mask-closable="false" @on-ok="save" @on-cancel="cancel">
            <charge-for ref="chargefor" :clientId="modal.current" v-if="modal.isEdit" />
        </Modal>

    </div>
</template>

<script>
import { getClients, charge } from 'api/client';
import chargeFor from './charge-for';
export default {
    name: 'charge',
    data() {
        return {
            cols: [
                {
                    type: 'selection',
                    align: 'center',
                    width: '70px'
                },
                {
                    title: '客户账户',
                    key: 'account'
                },
                {
                    title: '昵称',
                    key: 'customerName'
                },
                {
                    title: '手机',
                    key: 'mobile'
                },
                {
                    title: '性别',
                    key: 'gender',
                    render: (h, params) => {
                        return params.row.gender==1 ? '男' : '女';
                    }
                },
                {
                    title: '余额',
                    key: 'balance',
                    render:(h,params)=>{
                        return params.row.balance/100;
                    }
                },
                {
                    title: '操作',
                    key: 'action',
                    align: 'center',
                    render: (h, params) => {
                        return h('div', [
                            h('Button', {
                                props: {
                                    type: 'primary',
                                    size: 'small'
                                },
                                style: {
                                    marginRight: '5px'
                                },
                                on: {
                                    click: () => {
                                        this.charge(params.row)
                                    }
                                }
                            }, '充值'),
                            h('Button', {
                                props: {
                                    type: 'error',
                                    size: 'small'
                                },
                                on: {
                                    click: () => {
                                        this.clear(params.row)
                                    }
                                }
                            }, '清空')
                        ]);
                    }
                }
            ],
            searchApi: getClients,
            params: { name: '', phone: '', promoterName: '' },
            modal: {
                isEdit: false, title: '添加', current: null
            },

        }
    },
    components: {
        chargeFor
    },
    created() {
        this.$root.eventHub.$on('charge', () => {
            this.cancel();
        });
    },
    destroyed() {
        this.$root.eventHub.$off('charge');
    },
    methods: {
        //删除
        clear(model) {
            var table = this.$refs.list;
            this.$Modal.confirm({
                title: '清空警告', content: "确定要清空当前客户的余额么?",
                onOk: () => {
                    const parms = { id: model.id,money:0,cardCode:'' }
                    charge(parms).then(c => {
                        if (c.data.success) {
                            table.initData();
                        }
                    })
                }
            })
        },
        charge(row) {
            this.modal.current = row.id;
            this.modal.isEdit = true;
            this.modal.title = "编辑客户:" + row.customerName;
        },
        save() {
            this.$refs.chargefor.commit();
        },
        cancel() {
            this.modal.isEdit = false;
            this.modal.title = "添加客户";
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