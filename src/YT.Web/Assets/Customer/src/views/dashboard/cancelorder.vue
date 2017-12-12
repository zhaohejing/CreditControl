<template>
    <div class="cancelorder">
        <head-top-tag></head-top-tag>
        <div style="min-height:calc(100% - 92px)">
            <Row class="cancelTop">
                <Col span="24">
                <Icon type="chevron-left"></Icon>
                <a @click="returnUp">返回上一页</a>
                </Col>
            </Row>
            <div class="cancelMain">
                <table border="1" class="table">
                    <thead>
                        <tr>
                            <td colspan="2">取消订单</td>
                        </tr>
                    </thead>
                    <tbody>
                        <tr>
                            <td>订单号</td>
                            <td class="c373d41">{{ form.orderNum }}</td>
                        </tr>
                        <tr>
                            <td>订单名称</td>
                            <td class="c373d41">{{ form.productName }} </td>
                        </tr>
                    </tbody>
                </table>
                <Row class="enterCancel">
                    <col span="24"> 请输入取消订单原因
                    </col>
                </Row>
                <Row class="enterCancel">
                    <col span="24">
                    <Form ref="form" :model="form">
                        <FormItem prop="reason">
                            <Input v-model="form.cancelReason" type="textarea" :maxlength="600" :autosize="{minRows: 11,maxRows: 11}"></Input>
                        </FormItem>
                    </Form>
                    </col>
                </Row>
                <Row class="submitApply">
                    <col span="24">
                    <Button type="primary" @click="submitApply">提交申请</Button>
                    </col>
                </Row>
            </div>
        </div>
        <footer-tag></footer-tag>
    </div>
</template>

<script>
import HeadTopTag from "components/HeadTop";
import FooterTag from "components/Footer";
import { order, completeOrder } from "api/product";
export default {
  data() {
    return {
      order: null,
      form: {
        customerId: null,
        orderNum: "",
        state: false,
        cancelReason: "",
        productName: ""
      }
    };
  },
  components: {
    FooterTag,
    HeadTopTag
  },
  created() {
    this.Init();
  },
  activated() {
    this.Init();
  },
  methods: {
    Init() {
      const id = this.$route.query.id;
      const customerId = localStorage.getItem("Credit-Id");
      if (!customerId) {
        this.$router.push({ path: "/login" });
        return;
      }
      order({ id: id })
        .then(r => {
          if (r.data.success) {
            this.form.customerId = customerId;
            this.form.orderNum = r.data.result.orderNum;
            this.form.productName =
              r.data.result.productName + "(" + r.data.result.cate + ")";
          }
        })
        .catch(e => {
          this.$Message.error(e.response.data.error.message);
        });
    },
    // 返回上一页
    returnUp() {
      this.$router.push({ path: "/order", query: { id: null } });
    },
    // 提交申请
    submitApply() {
      // 删除
      this.$Modal.confirm({
        title: "操作提示",
        content: "确定要取消当前订单么?",
        onOk: () => {
          completeOrder(this.form)
            .then(c => {
              if (c.data.success) {
                this.$Message.success("操作成功");
                this.$router.push({ path: "/order", query: { id: null } });
              }
            })
            .catch(e => {
              this.$Message.error(e.response.data.error.message);
            });
        }
      });
    }
  }
};
</script>
<style rel="stylesheet/scss" lang="scss" scoped>
.cancelorder {
  height: 100%;
}

.cancelTop {
  width: 100%;
  padding: 52px 136px;
  a {
    color: #383d41;
  }
}

.cancelMain {
  width: 100%;
  text-align: center;
  .table {
    width: 480px;
    margin: 0 auto;
    border: 1px solid #ebebeb;
    border-collapse: collapse;
    margin-bottom: 54px;
    thead {
      background: #f7f7f7;
    }
    tr {
      height: 40px;
      line-height: 40px;
      border-bottom: 1px solid #ebebeb;
      td {
        font-size: 14px;
        color: #b5b5b5;
      }
      .c373d41 {
        color: #373d41;
      }
    }
  }
  .enterCancel {
    width: 600px;
    text-align: left;
    margin: 0 auto;
    color: #373d41;
    font-size: 14px;
    padding-bottom: 10px;
    textarea {
      width: 598px;
      height: 228px;
      border: 1px solid #ebebeb;
    }
    .ivu-form-item {
      margin-bottom: 0;
    }
  }
  .submitApply {
    padding-top: 44px;
    margin-bottom: 64px;
    button {
      width: 200px;
      height: 40px;
      text-align: center;
      line-height: 40px;
      background: #679fec;
      border-radius: 0;
      border: 1px solid #679fec;
      font-size: 14px;
      padding: 0;
    }
  }
}
</style>

