<template>
    <div class="animated fadeIn">
        <Row>
            <Col :span="8"> 单号:{{order.orderNum }}
            </Col>
            <Col :span="8"> 状态:{{order.state==null?'未完成':(order.state?'已完成':'已取消') }}
            </Col>
            <Col :span="8">
            <a @click="form" v-if="order.formId">查看表单</a>
            </Col>
        </Row>
        <Row>
            <table class="gridtable">
                <thead>
                    <tr>
                        <th>商品名称</th>
                        <th>单价</th>
                        <th>个数</th>
                        <th>总价</th>
                    </tr>
                </thead>
                <tbody>
                    <tr>
                        <td>{{order.productName}}</td>
                        <td>{{order.price}}</td>
                        <td>{{order.count}}</td>
                        <td>{{order.totalPrice}}</td>
                    </tr>
                </tbody>
            </table>
        </Row>
        <Row>
            <Col :span="8"> 公司名称:{{order.companyName }}
            </Col>
            <Col :span="8"> 联系人名称:{{order.contact }}
            </Col>
            <Col :span="8"> 商品总价:{{order.totalPrice }}
            </Col>
        </Row>
        <Row v-if="order.state!=null&&!order.state">
            <Col :span="2"> 订单取消原因
            </Col>
            <Col :span="22">
            <Input v-model="order.cancelReason" disabled placeholder=""></Input>
            </Col>
        </Row>
        <Row>
            <Col :span="4" offset="9">
            <Button type="ghost" @click="back" style="margin-left: 8px">返回</Button>
            <Button v-if="order.state==null" type="primary" @click="complete">完成</Button>
            </Col>

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
        complete() {
            this.$Modal.confirm({
                title: '操作提示', content: "确定要完成当前订单么?",
                onOk: () => {
                    const parms = { id: this.order.id }
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
        back() {
            this.$router.push({ path: '/orders' })
        },
        form(row) {
            this.modal.isShow = true;
        }
    }
}
</script>


<style type="text/css" scoped>
table.gridtable {
    width: 100%;
    font-family: verdana, arial, sans-serif;
    font-size: 11px;
    color: #333333;
    border-width: 1px;
    border-color: #666666;
    border-collapse: collapse;
}

table.gridtable th {
    border-width: 1px;
    padding: 8px;
    border-style: solid;
    border-color: #666666;
    background-color: #dedede;
}

table.gridtable td {
    border-width: 1px;
    padding: 8px;
    border-style: solid;
    border-color: #666666;
    background-color: #ffffff;
}
</style>
