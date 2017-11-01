<template>
    <div class="animated fadeIn">
        <Row>
            aaaaaaaa
        </Row>
    </div>
</template>

<script>
import { order, completeorder } from 'api/products';
export default {
    name: 'orderdetail',
    data() {
        return {
            order: null
        }
    },
    components: {
    },
    created() {
        this.init();
    },
    destroyed() {
    },
    methods: {
        init() {
            order({ id: this.$route.query.id }).then(r => {
                if (r.data.success) {
                    this.order = r.data.result;
                }
            }).catch(e => {
                this.$Message.error(e.response.data.error.message);
            })
        },
        // 完成
        complete(model) {

            this.$Modal.confirm({
                title: '操作提示', content: "确定要完成当前订单么?",
                onOk: () => {
                    const parms = { id: model.id }
                    completeorder(parms).then(c => {
                        if (c.data.success) {
                            this.$Message.success('提交成功');
                            this.$router.push({ path: '/orders' })
                        }
                    }).catch(e => {
                        this.$Message.error(e.response.data.error.message);
                    })
                }
            })
        },
        form(row) {
            this.modal.isShow = true;
        }
    }
}
</script>


<style type="text/css" scoped>

</style>