<template>
    <div>
        <Form ref="product" :model="current.product" :rules="ruleValidate" inline :label-width="120">
            <Row>
                <Col :span="16">
                <Row>
                    <Col :span="12">
                    <FormItem label="商品名称">
                        <Input v-model="current.product.id" placeholder="商品名称"></Input>
                    </FormItem>
                    </Col>
                    <Col :span="12">
                    <FormItem label="价格">
                        <Input v-model="current.product.id" placeholder="价格"></Input>
                    </FormItem>
                    </Col>
                </Row>

                <Row>
                    <Col :span="12">
                    <FormItem label="一级分类">
                        <Select v-model="current.product.id" placeholder="请选择">
                            <Option value="beijing">北京市</Option>
                            <Option value="shanghai">上海市</Option>
                            <Option value="shenzhen">深圳市</Option>
                        </Select>
                    </FormItem>
                    </Col>
                    <Col :span="12">
                    <FormItem label="二级分类">
                        <Select v-model="current.product.id" placeholder="请选择">
                            <Option value="beijing">北京市</Option>
                            <Option value="shanghai">上海市</Option>
                            <Option value="shenzhen">深圳市</Option>
                        </Select>
                    </FormItem>
                    </Col>
                </Row>

                <Row>
                    <FormItem label="商品简介">
                        <Input v-model="current.product.id" type="textarea" :autosize="{minRows: 2,maxRows: 5}" placeholder="商品简介"></Input>
                    </FormItem>
                </Row>

                </Col>
                <Col :span="8">
                <Upload multiple type="drag" action="//jsonplaceholder.typicode.com/posts/">
                    <div style="padding: 20px 0">
                        <Icon type="ios-cloud-upload" size="52" style="color: #3399ff"></Icon>
                        <p>点击或将文件拖拽到这里上传</p>
                    </div>
                </Upload>
                </Col>
            </Row>
            <Row>
                <vue-tinymce ref='tinymce' v-model='current.product.userName' :setting='settings'>

                </vue-tinymce>
            </Row>
        </Form>

    </div>
</template>
<script>
import { saveProduct, getProductForEdit } from 'api/products';
import { VueTinymce, TinymceSetting } from 'vue-tinymce';
export default {
    nmae: 'modifyProduct',
    props: {
        productId: {
            type: Number,
            default() {
                return null
            }
        }
    },
    components: {
        VueTinymce, TinymceSetting
    },
    data() {
        return {
            current: {
                product: {
                    id: this.productId,
                    name: '',
                    userName: '',
                    phoneNumber: '',
                    password: '',
                    isActive: true
                }
            },
            settings: Object.assign({}, TinymceSetting, {
                height: 200,
                language_url: './static/langs/zh_CN.js',
                block_formats: 'Paragraph=p;Heading 1=h1;Heading 2=h2;Heading 3=h3;Heading 4=h4;Heading 5=h5;Heading 6=h6;',
            }),
            ruleValidate: {
                name: [
                    { required: true, message: '姓名不可为空', trigger: 'blur' }
                ],
                userName: [
                    { required: true, message: '用户名不可为空', trigger: 'blur' }
                ],
                phoneNumber: [
                    { required: false, message: '手机不合法', trigger: 'blur' }
                ]

            }
        }
    },
    created() {
        this.init();
    },
    methods: {
        async init() {
            getProductForEdit({ id: this.current.product.id }).then(c => {
                if (c.data.success) {
                    this.current.product = c.data.result;
                }
            })
        },

        commit() {
            this.$refs.product.validate(valid => {
                if (valid) {
                    saveProduct(this.current).then(response => {
                        if (response.data.success) {
                            this.$root.eventHub.$emit('product');
                        } else {
                            this.$root.eventHub.$emit('product');
                        }
                    }).catch(erroe => {
                        this.$Message.error(erroe.error);
                        this.$root.eventHub.$emit('product');
                    });
                } else {
                    this.$Message.error('表单验证失败!');
                    this.$root.eventHub.$emit('product');
                }
            })
        }
    }
}
</script>
