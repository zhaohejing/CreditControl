<template>
    <div class="animated fadeIn">

        <Row>
            <Form ref="chat" :model="chat" :rules="ruleValidate">
                <FormItem label="发送内容" prop="content">
                    <Input v-model="chat.content" type="textarea"
                     :autosize="{minRows: 20,maxRows: 20}" placeholder="请输入..."></Input>
                </FormItem>
                <FormItem>
                    <Button type="primary" @click="save">发送</Button>
                    <Button type="ghost" @click="reset" style="margin-left: 8px">重置</Button>
                </FormItem>
            </Form>
        </Row>

    </div>
</template>

<script>
import { send } from 'api/chat';
export default {
    name: 'weChat',
    data() {
        return {
            chat:{
                content:''
            },
             ruleValidate: {
                content: [
                    { required: true, message: '内容不可为空', trigger: 'blur' }
                ]
              
            }
        }
    },
    methods: {
        save() {
             this.$refs.chat.validate((valid) => {
                if (valid) {
                    send(this.chat).then(r => {
                        if (r.data.success) {
                              this.chat.content="";
                        } else {
                            this.$Message.error('发送失败!');
                        }
                    });
                } else {
                    this.$Message.error('表单验证失败!');
                }
            })
        },
        reset() {
            this.chat.content="";
        }
    },
    mounted() {
       // const token = this.$store.getters.token;

        // this.$Notice.success({
        //     title: '欢迎使用 WZ 框架',
        //     desc: `你的账户权限是 ${token} 
        //                     <br>
        //                     喜欢就去github给个 start 鼓励一下吧`,
        //     duration: 10
        // });

    }
}
</script>


<style type="text/css" scoped>
.time {
    font-size: 14px;
    font-weight: bold;
}

.content {
    padding-left: 5px;
}

.dashboard_feature {
    text-align: center;
}

.demo-carousel {
    height: 600px;
    line-height: 200px;
    text-align: center;
    color: #fff;
    font-size: 20px;
    background: #506b9e;
}

.demo-carousel img {
    height: 100%;
    width: 100%;
}

h3,
h4,
h5 {
    margin: 12px;
}

p {
    margin: 12px;
}

.row-margin-top {
    margin-top: 30px;
}
</style>