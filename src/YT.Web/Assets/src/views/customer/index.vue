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
                <template slot="actions">
                    <Button @click="add" type="primary">添加</Button>
                </template>
            </milk-table>
        </Row>
        <!-- 添加和编辑窗口 -->
        <Modal :width="1000" :transfer="false" v-model="modal.isEdit" :title="modal.title" :mask-closable="false" @on-ok="save" @on-cancel="cancel">
            <modify-client ref="client" :clientId="modal.current" v-if="modal.isEdit" />
        </Modal>

    </div>
</template>

<script>
import { getClients, deleteClient, getClientForEdit, updateClient, updateStraw } from 'api/client';
import modifyClient from './modify-client';
export default {
    name: 'role',
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
                    title: '推广专员',
                    key: 'promoterName'
                },
                {
                    title: '性别',
                    key: 'gender',
                    render: (h, params) => {
                        return params.row.gender == 1 ? '男' : '女';
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
                    width: '300px',
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
                                style: {
                                    marginRight: '5px'
                                },
                                on: {
                                    click: () => {
                                        this.delete(params.row)
                                    }
                                }
                            }, '删除'),
                            h('Button', {
                                props: {
                                    type: 'info',
                                    size: 'small'
                                },
                                on: {
                                    click: () => {
                                        this.updateStraw(params.row)
                                    }
                                }
                            }, '添加吸管')
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
        modifyClient
    },
    created() {
        this.$root.eventHub.$on('client', () => {
            this.cancel();
        });
    },
    destroyed() {
        this.$root.eventHub.$off('client');
    },
    methods: {
        //删除
        delete(model) {
            var table = this.$refs.list;
            this.$Modal.confirm({
                title: '删除提示', content: "确定要删除当前客户么?",
                onOk: () => {
                    const params = { id: model.id }
                    deleteClient(params).then(c => {
                        if (c.data.success) {
                            table.initData();
                        }
                    })
                }
            })
        },
        updateStraw(row) {
            var table = this.$refs.list;
            const params = { id: row.id }
            updateStraw(params).then(c => {
                if (c.data.success) {
                     this.$Message.success('添加吸管成功!');
                    table.initData();
                }
            })
        },
        add() {
            this.modal.isEdit = true;
            this.modal.title = "添加客户";
        },
        edit(row) {
            this.modal.current = row.id;
            this.modal.isEdit = true;
            this.modal.title = "编辑客户:" + row.customerName;
        },
        save() {
            this.$refs.client.commit();
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