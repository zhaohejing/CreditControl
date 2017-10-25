<template>
    <div class="animated fadeIn">
        <Row>
            <milk-table ref="list" :layout="[14,6,4]" :columns="cols" :search-api="searchApi" :params="params">
                <template slot="search">
                    <Form ref="params" :model="params" inline :label-width="60">
                        <FormItem label="推广员">
                            <Input v-model="params.name" placeholder="推广员"></Input>
                        </FormItem>
                        <FormItem label="手机">
                            <Input v-model="params.mobile" placeholder="手机"></Input>
                        </FormItem>
                    </Form>
                </template>
                <template slot="actions">
                    <Button @click="add" type="primary">添加</Button>
                </template>
            </milk-table>
        </Row>
        <!-- 添加和编辑窗口 -->
        <Modal  :transfer="false" v-model="modal.isEdit" :title="modal.title" :mask-closable="false" @on-ok="save" @on-cancel="cancel">
            <modify-promoter ref="promoter" :role="modal.current" v-if="modal.isEdit" />
        </Modal>
    </div>
</template>

<script>
import { getPromoters, deletePromoter,deletePromoters } from 'api/promoter';
import modifyPromoter from './modify-promoter';
export default {
    name: 'promoter',
    data() {
        return {
            cols: [
                {
                    type: 'selection',
                    align: 'center',
                    width: '70px'
                },
                {
                    title: '推广员',
                    key: 'promoterName'
                },
                {
                    title: '手机',
                    key: 'mobile'
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
                    width:'210px',
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
                                        this.delete(params.row)
                                    }
                                }
                            }, '生成二维码')
                        ]);
                    }
                }
            ],
            searchApi: getPromoters,
            params: { name: '', mobile: '' },
            modal: {
                isEdit: false, title: '添加', current: null
            },

        }
    },
    components: {
        modifyPromoter
    },
    created() {
        this.$root.eventHub.$on('promoter', () => {
            this.cancel();
        });
    },
    destroyed() {
        this.$root.eventHub.$off('promoter');
    },
    methods: {
        //删除
        delete(model) {
            var table = this.$refs.list;
            this.$Modal.confirm({
                title: '删除提示', content: "确定要删除当前推广员么?",
                onOk: () => {
                    const parms = { id: model.id }
                    deletePromoter(parms).then(c => {
                        if (c.data.success) {
                            table.initData();
                        }
                    })
                }
            })
        },
        add() {
            this.modal.isEdit = true;
            this.modal.title = "添加推广员";
        },
        edit(row) {
            this.modal.current = row.id;
            this.modal.isEdit = true;
            this.modal.title = "编辑推广员:" + row.displayName;
        },
        save() {
            this.$refs.promoter.commit();
        },
        cancel() {
            this.modal.isEdit = false;
            this.modal.title = "添加推广员";
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