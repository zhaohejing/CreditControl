<template>
    <div>
        <Form ref="promoter" :model="promoter.promoterEditDto" :rules="ruleValidate" :label-width="100">
            <FormItem label="推广员" prop="promoterName">
                <Input v-model="promoter.promoterEditDto.promoterName" placeholder="推广员"></Input>
            </FormItem>
            <FormItem label="手机" prop="mobile">
                <Input v-model="promoter.promoterEditDto.mobile" placeholder="手机"></Input>
            </FormItem>
            <FormItem label="是否启用">
                <i-switch size="large" v-model="promoter.promoterEditDto.isActive">
                    <span slot="open">启用</span>
                    <span slot="close">禁用</span>
                </i-switch>
            </FormItem>
        </Form>
    </div>
</template>
<script>
import { getPromoterForEdit, updatePromoter, } from 'api/promoter';

export default {
    name: 'modify-promoter',
    props: {
        promoterId: {
            type: Number,
            default() {
                return null
            }
        }
    },
    data() {
        return {
            promoter: {
                promoterEditDto: {
                    id: this.promoterId,
                    promoterName: "",
                    mobile: "",
                    shareUrl: "",
                    isActive: true
                }
            },
            ruleValidate: {
                promoterName: [
                    { required: true, message: '推广员不能为空', trigger: 'blur' }
                ]
              
            }
        }
    },
    created() {
        this.init();
    },
    mounted() {

    },
    methods: {
        async init() {
            getPromoterForEdit({ id: this.promoter.promoterEditDto.id }).then(c => {
                if (c.data.success) {
                    this.promoter.promoterEditDto = c.data.result.promoter;
                }
            })
        },
        commit() {
            this.$refs.promoter.validate((valid) => {
                if (valid) {
                    updatePromoter(this.promoter).then(r => {
                        if (r.data.success) {
                            this.$root.eventHub.$emit('promoter');
                        } else {
                            this.$root.eventHub.$emit('promoter');
                        }
                    });
                } else {
                    this.$Message.error('表单验证失败!');
                    this.$root.eventHub.$emit('promoter');
                }
            })
        }
    }
}
</script>
