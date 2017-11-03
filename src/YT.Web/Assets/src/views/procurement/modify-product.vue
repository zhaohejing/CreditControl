<template>
    <div>
        <Form ref='product' :model='current.productEditDto' :rules='ruleValidate' inline :label-width='120'>
            <Row>
                <Col :span='18'>
                <Row>
                    <Col :span='12'>
                    <FormItem label='商品名称' prop='productName'>
                        <Input v-model='current.productEditDto.productName' placeholder='商品名称'></Input>
                    </FormItem>
                    </Col>
                    <Col :span='12'>
                    <FormItem label='价格' prop='price'>
                        <InputNumber :min='0' v-model='current.productEditDto.price' placeholder='价格'></InputNumber>
                    </FormItem>
                    </Col>
                </Row>

                <Row>
                    <Col :span='12'>
                    <FormItem label='一级分类'>
                        <Select @on-change='pick' v-model='current.productEditDto.levelOneId' style='width:150px'>
                            <Option v-for='item in Series.OneList' :value='item.id' :key='item.id'>{{ item.name }}</Option>
                        </Select>
                    </FormItem>
                    </Col>
                    <Col :span='12'>
                    <FormItem label='二级分类'>
                        <Select v-model='current.productEditDto.levelTwoId' style='width:150px'>
                            <Option v-for='item in Series.TwoList' :value='item.id' :key='item.id'>{{ item.name }}</Option>
                        </Select>
                    </FormItem>
                    </Col>
                </Row>

                <Row>
                    <FormItem label='商品简介'>
                        <Input style='width:400px' v-model='current.productEditDto.description' type='textarea' :autosize='{minRows: 2,maxRows: 5}' placeholder='商品简介'></Input>
                    </FormItem>
                </Row>

                </Col>
                <Col :span='6'>
                <Upload multiple type='drag' :on-error='error' :on-success='success' :headers='upload.headers' :action='upload.url'>
                    <div style='padding: 20px 0'>
                        <Icon type='ios-cloud-upload' size='52' style='color: #3399ff'></Icon>
                        <p>点击或将文件拖拽到这里上传</p>
                    </div>
                </Upload>
                </Col>
            </Row>
            <Row>
                <vue-tinymce ref='tinymce' v-model='current.productEditDto.content' :setting='settings'>

                </vue-tinymce>
            </Row>
        </Form>

    </div>
</template>
<script>
import { saveProduct, getProductForEdit } from 'api/products';
import { VueTinymce, TinymceSetting } from 'vue-tinymce';
import { getCates } from 'api/cate';
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
                productEditDto: {
                    id: this.productId,
                    productName: '',
                    price: 0,
                    levelOneId: 0,
                    levelTwoId: 0,
                    description: '',
                    content: '',
                    isActive: true,
                    profile: ''
                }
            },
            upload: {
                url: 'http://47.93.2.82:9999/api/File/ImageUpload',
                headers: { Authorization: 'Bearer ' + this.$store.getters.token }

            },
            Series: {
                One: null, OneList: null, TwoList: null
            },
            settings: Object.assign({}, TinymceSetting, {
                height: 200,
                language_url: './static/langs/zh_CN.js',
                block_formats: 'Paragraph=p;Heading 1=h1;Heading 2=h2;Heading 3=h3;Heading 4=h4;Heading 5=h5;Heading 6=h6;',
            }),
            ruleValidate: {
                productName: [
                    { required: true, message: '产品名不可为空', trigger: 'blur' }
                ]
            }
        }
    },
    created() {
        this.init();
        this.initCate();
    },
    methods: {
        async init() {
            getProductForEdit({ id: this.current.productEditDto.id }).then(c => {
                if (c.data.success) {
                    this.current.productEditDto = c.data.result.product;
                }
            })

        },
        async initCate() {
            getCates({ id: null }).then(c => {
                if (c.data.success && c.data.result) {
                    this.Series.OneList = c.data.result;
                }
            })
        },
        pick(current) {
            getCates({ id: current }).then(c => {
                if (c.data.success && c.data.result) {
                    this.Series.TwoList = c.data.result;
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
        },
        error(error, file, filelist) {
            if (!file.success) {
                this.$Message.error(file.error.message);
            }
        },
        success(response, file, filelist) {
            if (response.success) {
                this.current.productEditDto.profile = response.result[0].guid;
            }
        }
    }
}
</script>
