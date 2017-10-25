<template>
    <div class="animated fadeIn">
        <Row>
            <milk-table ref="list" :layout="[14,6,4]" :columns="cols" :search-api="searchApi" :params="params">
                <template slot="search">
                    <Form ref="params" :model="params" inline :label-width="60">
                        <FormItem label="充值卡">
                            <Input v-model="params.code" placeholder="充值卡"></Input>
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
            <Form ref="card" :model="current.cardEditDto" inline :label-width="70">
                <FormItem label="金额" prop="money">
                    <InputNumber :precision="0" v-model="current.cardEditDto.money" style="width:200px;" :min="1" :step="1" placeholder="请输入金额"></InputNumber>
                </FormItem>
            </Form>
        </Modal>

    </div>
</template>

<script>
import { getCards, updateCard, getCardForEdit, deleteCard } from 'api/card';
export default {
    name: 'card',
    data() {
        return {
            cols: [
                {
                    type: 'selection',
                    align: 'center',
                    width: '70px'
                },
                {
                    title: '充值卡号码',
                    key: 'cardCode'
                },
                {
                    title: '金额',
                    key: 'money',
                    render: (h, params) => {
                        return params.row.money / 100;
                    }
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
            searchApi: getCards,
            params: { code: '', state: null },
            modal: {
                isEdit: false,
                title: '添加',
            },
            current: {
                cardEditDto: {
                    money: 0,
                    isUsed: false
                }
            }

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
                    deleteCard(parms).then(c => {
                        if (c.data.success) {
                            table.initData();
                        }
                    })
                }
            })
        },
        add() {
            this.modal.isEdit = true;
            this.modal.title = "添加充值卡";
        },
        save() {
            var table = this.$refs.list;
            if (!this.current.cardEditDto.money) {
                this.$Message.error({
                    message: '金额不可为空',
                    duration: 5 * 1000,
                    closable: true
                });
                return;
            }
            updateCard(this.current).then(r => {
                if (r.data.success) {
                    table.initData();
                } else {
                    table.initData();
                }
            });
        },
        cancel() {
            this.modal.isEdit = false;
            this.modal.title = "添加充值卡";
            this.current.cardEditDto = {
                money: 0,
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