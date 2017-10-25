<template>
    <div class="animated fadeIn">
        <Row>
            <milk-table ref="list"  :layout="[17,4,3]" :columns="cols" :search-api="searchApi" :params="params">
                <template slot="search">
                    <Form ref="params" :model="params" inline :label-width="60">
                        <FormItem label="用户姓名">
                            <Input v-model="params.name" placeholder="请输入用户姓名"></Input>
                        </FormItem>
                        <FormItem label="手机号码">
                            <Input v-model="params.phone" placeholder="请输入手机号码"></Input>
                        </FormItem>
                        <FormItem label="权限角色">
                            <Select v-model="params.role">
                                <Option v-for="c in roles" :value="c.id" :key="c.id">{{c.displayName}}</Option>
                            </Select>
                        </FormItem>
                    </Form>
                </template>
                <template slot="actions">
                    <Button @click="add" type="primary">添加</Button>
                </template>
            </milk-table>
        </Row>
        <!-- 添加和编辑窗口 -->
        <Modal :width="800" :transfer="false" v-model="modal.isEdit" :title="modal.title" :mask-closable="false" @on-ok="save" @on-cancel="cancel">
            <modify-account ref="account" :user="modal.current" v-if="modal.isEdit" />
        </Modal>

    </div>
</template>

<script>
import { getUsers, getRoles, getUserForEdit, deleteUser } from 'api/manage';
import modifyAccount from './modify-account';
export default {
    name: 'account',
    data() {
        return {
            cols: [
                {
                    type: 'selection',
                    align: 'center',
                    width: '70px'
                },
                {
                    title: '账户',
                    key: 'userName'
                },

                {
                    title: '用户姓名',
                    key: 'name'
                },
                {
                    title: '手机号',
                    key: 'phoneNumber'
                },
                {
                    title: '角色',
                    key: 'roles',
                    render: (h, params) => {
                        let names = "";
                        if (params.row.roles) {
                            params.row.roles.forEach(c => {
                                names += c.roleName + ',';
                            })
                        }
                        return names;
                    }
                },
                {
                    title: '状态',
                    key: 'isActive',
                    render: (h, params) => {
                        return params.row.isActive ? '启用' : '禁用';
                    }
                },
                {
                    title: '创建时间',
                    key: 'creationTime',
                    render: (h, params) => {
                        return this.$fmtTime(params.row.creationTime);
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
                                        this.edit(params.row)
                                    }
                                }
                            }, '编辑'),
                            h('Button', {
                                props: {
                                    type: 'error',
                                    size: 'small'
                                },
                                on: {
                                    click: () => {
                                        this.delete(params.row)
                                    }
                                }
                            }, '删除')
                        ]);
                    }
                }
            ],
            searchApi: getUsers,
            params: { name: '', phone: '', role: null },
            modal: {
                isEdit: false, title: '添加', current: null
            },
            roles: []
        }
    },
    components: {
        modifyAccount
    },
    created() {
        var self=this;
        self.$root.eventHub.$on('account', () => {
            self.cancel();
        });
        self.initRoles();
    },
    destroyed() {
        this.$root.eventHub.$off('account');
    },
    methods: {
        //删除
        delete(model) {
            var table = this.$refs.list;
            this.$Modal.confirm({
                title: '删除提示', content: "确定要删除当前用户么?",
                onOk: () => {
                    const parms = { id: model.id }
                    deleteUser(parms).then(c => {
                        if (c.data.success) {
                            table.initData();
                        }
                    })
                }
            })
        },
        add() {
            this.modal.isEdit = true;
            this.modal.title = "添加用户";
        },
        edit(row) {
            this.modal.current = row.id;
            this.modal.isEdit = true;
            this.modal.title = "编辑用户:" + row.name;
        },
        save() {
            this.$refs.account.commit();
        },
        cancel() {
            this.modal.isEdit = false;
            this.modal.title = "添加用户";
            this.modal.current = null;
            this.$refs.list.initData();
        },
        initRoles() {
            getRoles().then(c => {
                if (c.data.success) {
                    this.roles = c.data.result.items;
                }
            })
        }
    },
    mounted() {
    }
}
</script>


<style type="text/css" scoped>

</style>