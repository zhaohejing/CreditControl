<template>
    <div class='animated fadeIn'>
        <Row>
            <milk-table ref='list' :layout='[17,4,3]' :columns='cols' :search-api='searchApi' :params='params'>
                <template slot='search'>
                    <Form ref='params' :model='params' inline :label-width='60'>
                        <FormItem label='产品名称'>
                            <Input v-model='params.name' placeholder='请输入产品名称'></Input>
                        </FormItem>
                        <FormItem label='状态'>
                            <Select v-model='params.state'>
                                <Option value="">全部</Option>
                                <Option value="true">上架</Option>
                                <Option value="false">下架</Option>
                            </Select>
                        </FormItem>
                        <FormItem label='一级分类'>
                            <Select v-model='params.levelOne'>
                                <Option v-for='c in roles' :value='c.id' :key='c.id'>{{c.displayName}}</Option>
                            </Select>
                        </FormItem>
                        <FormItem label='二级分类'>
                            <Select v-model='params.levelTwo'>
                                <Option v-for='c in roles' :value='c.id' :key='c.id'>{{c.displayName}}</Option>
                            </Select>
                        </FormItem>
                    </Form>
                </template>
                <template slot='actions'>
                    <Button @click='add' type='primary'>添加</Button>
                </template>
            </milk-table>
        </Row>
        <!-- 添加和编辑窗口 -->
        <Modal :width='800' :transfer='false' v-model='modal.isEdit' :title='modal.title' :mask-closable='false' @on-ok='save' @on-cancel='cancel'>
            <modify-product ref='product' :productId='modal.current' v-if='modal.isEdit' />
        </Modal>

    </div>
</template>

<script>
import { getProducts, deleteProduct } from 'api/products';
import modifyProduct from './modify-product';
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
                    title: '商品名称',
                    key: 'productName'
                },

                {
                    title: '创建时间',
                    key: 'creationTime',
                    render: (h, params) => {
                        return this.$fmtTime(params.row.creationTime);
                    }
                },
                {
                    title: '价格',
                    key: 'price'
                },
                {
                    title: '一级分类',
                    key: 'levelOneName'
                },
                {
                    title: '二级分类',
                    key: 'levelTwoName'
                },
                {
                    title: '状态',
                    key: 'isActive',
                    render: (h, params) => {
                        return params.row.state ? '上架' : '下架';
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
                            }, '修改'),
                            h('Button', {
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
                                        this.edit(params.row)
                                    }
                                }
                            }, '上架'),
                            h('Button', {
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
                                        this.edit(params.row)
                                    }
                                }
                            }, '下架'),
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
            searchApi: getProducts,
            params: { name: '', state: null, levelOne: null, levelTwo: null },
            modal: {
                isEdit: false, title: '添加', current: null
            },
            roles: []
        }
    },
    components: {
        modifyProduct
    },
    created() {
        this.$root.eventHub.$on('product', () => {
            this.cancel();
        });
    },
    destroyed() {
        this.$root.eventHub.$off('product');
    },
    methods: {
        // 删除
        delete(model) {
            const table = this.$refs.list;
            this.$Modal.confirm({
                title: '删除提示', content: '确定要删除当前用户么?',
                onOk: () => {
                    const parms = { id: model.id }
                    deleteProduct(parms).then(c => {
                        if (c.data.success) {
                            table.initData();
                        }
                    })
                }
            })
        },
        add() {
            this.modal.isEdit = true;
            this.modal.title = '添加商品';
        },
        edit(row) {
            this.modal.current = row.id;
            this.modal.isEdit = true;
            this.modal.title = '编辑商品:' + row.productName;
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