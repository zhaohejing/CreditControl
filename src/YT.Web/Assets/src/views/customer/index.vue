<template>
    <div class='animated fadeIn'>
        <Row>
            <milk-table ref='list' :layout='[18,3,3]' :columns='cols' :search-api='searchApi' :params='params'>
                <template slot='search'>
                    <Form ref='params' :model='params' inline :label-width='60'>
                        <FormItem label='公司名称'>
                            <Input v-model='params.companyName' placeholder='请输入公司名称'></Input>
                        </FormItem>
                        <FormItem label='联系人'>
                            <Input v-model='params.contact' placeholder='联系人'></Input>
                        </FormItem>
                        <FormItem label='所在城市'>
                            <Input v-model='params.city' placeholder='所在城市'></Input>
                        </FormItem>
                    </Form>
                </template>
            </milk-table>
        </Row>
        <!-- 添加和编辑窗口 -->
        <Modal :width='800' :transfer='false' v-model='modal.isEdit' :title='modal.title' :mask-closable='false' @on-ok='save' @on-cancel='cancel'>
             <Form ref="cate" :model="modal.current" :rules="ruleValidate" inline :label-width="120">
                <FormItem label="分类名称" prop="name">
                    <Input v-model="modal.current.name" placeholder="分类名称"></Input>
                </FormItem>
            </Form>
        </Modal>

    </div>
</template>

<script>
import { getCustomers, deleteCustomer,auditCustomer,chargeCustomer,resetCustomer } from 'api/customer';
export default {
    name: 'customer',
    data() {
        return {
            cols: [
                {
                    title: '公司名称',
                    key: 'companyName'
                },
                {
                    title: '联系人',
                    key: 'contact'
                },
                {
                    title: '所在城市',
                    key: 'city'
                },
                {
                    title: '余额',
                    key: 'balance'
                },
                {
                    title: '详细地址',
                    key: 'address'
                },
                {
                    title: '状态',
                    key: 'state',
                    render: (h, params) => {
                        return params.row.state ? '已审核' : '未审核';
                    }
                },
                {
                    title: '操作',
                    key: 'action',
                    align: 'center',
                    width: '250px',
                    render: (h, params) => {
                        let children = [];
                        children.push(h('Button', {
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
                        }, '详情')) //组件1
                        children.push(h('Button', {
                            props: {
                                type: 'primary',
                                size: 'small'
                            },
                            style: {
                                marginRight: '5px',
                                hidden: !params.row.state
                            },
                            on: {
                                click: () => {
                                    this.delete(params.row)
                                }
                            }
                        }, '删除')) //组件3
                        if (!params.row.state) {
                            children.push(h('Button', {
                                props: {
                                    type: 'primary',
                                    size: 'small'
                                },
                                style: {
                                    marginRight: '5px',
                                    hidden: params.row.state
                                },
                                on: {
                                    click: () => {
                                        this.audit(params.row)
                                    }
                                }
                            }, '审核')) //组件3
                        } else {
                            children.push(h('Button', {
                                props: {
                                    type: 'error',
                                    size: 'small'
                                },
                                on: {
                                    click: () => {
                                        this.charge(params.row)
                                    }
                                }
                            }, '充值')) //组件2
                            children.push(h('Button', {
                                props: {
                                    type: 'error',
                                    size: 'small'
                                },
                                on: {
                                    click: () => {
                                        this.delete(params.row)
                                    }
                                }
                            }, '重置密码')) //组件2
                        }

                        return h('div', children)
                    }
                }
            ],
            searchApi: getCustomers,
            params: { companyName: '', contact: '', city: '' },
            modal: {
                isEdit: false, title: '添加', current: null
            }
        }
    },
    components: {
    },
    created() {
        this.$root.eventHub.$on('customer', () => {
            this.cancel();
        });
    },
    destroyed() {
        this.$root.eventHub.$off('customer');
    },
    methods: {
        // 删除
        delete(model) {
            const table = this.$refs.list;
            this.$Modal.confirm({
                title: '删除提示', content: '确定要删除当前客户么?',
                onOk: () => {
                    const parms = { id: model.id }
                    deleteCustomer(parms).then(c => {
                        if (c.data.success) {
                            table.initData();
                        }
                    })
                }
            })
        },
        audit(row){

        },
        charge(row){

        },
        save() {
            this.$refs.product.commit();
        },
        cancel() {
            this.modal.isEdit = false;
            this.modal.title = '添加用户';
            this.modal.current = null;
            this.$refs.list.initData();
        }

    }
}
</script>

<style type='text/css' scoped>

</style>