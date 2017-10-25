<template>
    <div class="animated fadeIn">
        <Row>
            <milk-table ref="list" :layout="[14,6,4]" :columns="cols" :search-api="searchApi" :params="params">
                <template slot="search">
                    <Form ref="params" :model="params" inline :label-width="60">
                        <FormItem label="唯鲜卡">
                            <Input v-model="params.code" placeholder="唯鲜卡"></Input>
                        </FormItem>
                        <FormItem label="状态">
                            <Select v-model="params.state" style="width:200px" clearable>
                                <Option value="true" label="已使用">
                                    <span>已使用</span>
                                </Option>
                                <Option value="false" label="未使用">
                                    <span>未使用</span>
                                </Option>

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
        <Modal :transfer="false" v-model="modal.isEdit" :title="modal.title" :mask-closable="false" @on-ok="save" @on-cancel="cancel">
            <Form ref="specialCard" :model="current.specialCardEditDto" :rules="ruleValidate" inline :label-width="100">
                <FormItem label="唯鲜卡号" prop="cardCode">
                    <Input v-model="current.specialCardEditDto.cardCode" placeholder="请输入唯鲜卡号"></Input>
                </FormItem>
                <FormItem label="密码" prop="password">
                    <Input v-model="current.specialCardEditDto.password" placeholder="请输入密码"></Input>
                </FormItem>
            </Form>
        </Modal>
    </div>
</template>

<script>
import { getSpecialCards, updateSpecialCard, getSpecialCardForEdit, deleteSpecialCard } from 'api/specialcard';
export default {
    name: 'specialcard',
    data() {
        return {
            cols: [
                {
                    type: 'selection',
                    align: 'center',
                    width: '70px'
                },
                {
                    title: '唯鲜卡',
                    key: 'cardCode'
                },
                {
                    title: '密码',
                    key: 'password'
                },
                {
                    title: '状态',
                    key: 'isUsed',
                    render: (h, params) => {
                        return params.row.isUsed ? '已使用' : '未使用';
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
                                        this.edit(params.row)
                                    }
                                }
                            }, '编辑')
                        ]);
                    }
                }
            ],
            searchApi: getSpecialCards,
            params: { code: '', state: null },
            modal: {
                isEdit: false,
                title: '添加',
            },
            current: {
                specialCardEditDto: {
                    id: null,
                    cardCode: "",
                    password: "",
                    isActive: true,
                    isUsed: false
                }
            },
            ruleValidate: {
                cardCode: [
                    { required: true, message: '唯鲜卡号不可为空', trigger: 'blur' }
                ],
                password: [
                    { required: true, message: '密码不能为空', trigger: 'blur' }
                ]
            },

        }
    },
    created() {

    },
    destroyed() {
    },
    methods: {
        //删除
        delete(model) {
            var table = this.$refs.list;
            let content = `确定要删除当前${model.isUsed ? '已使用' : '未使用'}的充值卡么。`
            this.$Modal.confirm({
                title: '删除提示', content: content,
                onOk: () => {
                    const parms = { id: model.id }
                    deleteSpecialCard(parms).then(c => {
                        if (c.data.success) {
                            table.initData();
                        }
                    })
                }
            })
        },
        edit(row) {
            if (row.id) {
                getSpecialCardForEdit({ id: row.id }).then(c => {
                    this.current.specialCardEditDto = c.data.result.specialCard;
                     this.modal.isEdit = true;
                 this.modal.title = "编辑唯鲜卡:"+this.current.specialCardEditDto.cardCode;
                }).catch(e => {
                    this.$Message.error({
                        message: e.data.error,
                        duration: 5 * 1000,
                        closable: true
                    });
                });
            }
        },
        add() {
            this.modal.isEdit = true;
            this.modal.title = "添加唯鲜卡";
        },
        save() {
            var table = this.$refs.list;
            this.$refs.specialCard.validate((valid) => {
                if (valid) {
                    updateSpecialCard(this.current).then(r => {
                        if (r.data.success) {
                            this.current.specialCardEditDto = {
                                id: null,
                                cardCode: "",
                                password: "",
                                isActive: true,
                                isUsed: false
                            };
                            table.initData();
                        } else {
                            table.initData();
                        }
                    });
                } else {
                    this.$Message.error('表单验证失败!');
                }
            })
        },
        cancel() {
            this.modal.isEdit = false;
            this.modal.title = "添加唯鲜卡";
            this.current.specialCardEditDto = {
                id: null,
                cardCode: "",
                password: "",
                isActive: true,
                isUsed: false
            };
            this.$refs.list.initData();
        }
    },
    mounted() {
    }
}
</script>


<style type="text/css" scoped>

</style>