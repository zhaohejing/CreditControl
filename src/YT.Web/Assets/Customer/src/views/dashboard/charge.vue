<template>
    <div class="charge">
        <head-top-tag></head-top-tag>
        <div class="chargeMain">
            <div class="chargeBox">
                <Row class="return">
                    <col span="24">
                    <Icon type="chevron-left"></Icon>
                    <a @click="returnUp">返回上一页</a>
                    </col>
                </Row>
                <div class="chargeCon">
                    <div class="account">
                        ￥{{ apply.balance }}
                        <span>账户余额</span>
                    </div>
                    <Form ref="form" :model="apply" label-position="right" :rules="ruleCustom" :label-width="80">
                        <FormItem label="公司名称">
                            <Input disabled v-model="apply.customerName"></Input>
                        </FormItem>
                        <FormItem label="充值金额" prop="chargeMoney">
                            <Input type="text" v-model="apply.chargeMoney"></Input>
                        </FormItem>
                        <FormItem>

                        </FormItem>
                    </Form>
                    <div class="explain">
                        <h4>
                            <span>!</span>充值说明</h4>
                        <p>请在充值金额栏中输入您此次充值的金额，并在汇款至下列账户，并在备注中输入您公司名称，我们将在第一时间确认您的充值金额。</p>
                        <p>汇款账户：{{ remitAccount }}</p>
                        <p>
                            <span>收款人</span>：{{ payee }}</p>
                        <p>
                            <span>开户行</span>：{{ bank }}</p>
                    </div>
                    <Row class="g-btn">
                        <col span="24">
                        <Button type="primary" @click="submit">提交</Button>
                        </col>
                    </Row>
                </div>
            </div>
        </div>
        <footer-tag></footer-tag>
    </div>
</template>

<script>
import HeadTopTag from "components/HeadTop";
import FooterTag from "components/Footer";
import { info, apply } from "api/login";
export default {
  data() {
    const validateNum = (rule, value, callback) => {
      if (!Number.isInteger(+value)) {
        callback(new Error("请输入合法数值"));
      } else {
        callback();
      }
      // 模拟异步验证效果
      setTimeout(() => {
        if (!Number.isInteger(value)) {
          callback(new Error("请输入数字值"));
        }
      }, 1000);
    };
    return {
      apply: {
        id: null,
        customerId: 0,
        customerName: "",
        chargeMoney: 0,
        balance: 0,
        actionName: "",
        state: null
      },
      ruleCustom: {
        chargeMoney: [
          { required: true, message: "充值金额不能为空", trigger: "blur" },
          { validator: validateNum, trigger: "blur" }
        ]
      },
      remitAccount: "6217 5801 0000 9086 612",
      payee: "雷有福",
      bank: "中国银行北京上地支行"
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
      const id = localStorage.getItem("Credit-Id");
      if (!id) {
        this.$router.push({ path: "/login" });
        return;
      }
      info({ id: id }).then(r => {
        if (r.data.success) {
          this.apply.customerId = r.data.result.id;
          this.apply.customerName = r.data.result.companyName;
          this.apply.balance = r.data.result.balance;
        }
      });
    },
    // 返回上一页
    returnUp() {
      this.$router.push({ path: "/dashboard" });
    },
    submit() {
      this.$refs.form.validate(valid => {
        if (valid) {
          apply(this.apply).then(r => {
            if (r.data.success) {
              this.$Message.success("提交成功!");
              this.$router.push({ path: "/support" });
            }
          });
        } else {
          this.$Message.error("表单验证失败!");
        }
      });
    }
  }
};
</script>

<style rel="stylesheet/scss" lang="scss">
.charge {
  .chargeMain {
    padding: 40px 0;
    background: #f5f5f6;
    .chargeBox {
      width: 996px;
      background: #fff;
      margin: 0 auto;
      padding: 36px 0 52px;
      .return {
        padding-left: 30px;
        a {
          color: #383d41;
        }
      }
      .chargeCon {
        width: 436px;
        margin: 0 auto;
        .account {
          width: 140px;
          height: 140px;
          text-align: center;
          margin: 0 auto;
          line-height: 150px;
          border: 1px solid #44699c;
          border-radius: 50%;
          font-size: 18px;
          color: #373d41;
          position: relative;
          margin-bottom: 25px;
          span {
            width: 56px;
            height: 56px;
            display: block;
            background: #679feb;
            color: #444;
            font-size: 13px;
            text-align: center;
            line-height: 16px;
            padding: 12px;
            border-radius: 50%;
            color: #fff;
            position: absolute;
            top: -9px;
            left: -9px;
          }
        }
        input {
          width: 320px;
          height: 40px;
          border-radius: 0;
          color: #4d4d4d;
          font-size: 14px;
          border: 1px solid #c3c5c6;
        }
        label {
          padding-top: 13px;
          font-size: 14px;
          color: #9b9ea0;
        }
        .ivu-form-item-required .ivu-form-item-label:before {
          display: none;
        }
        .explain {
          width: 460px;
          background: #f8f9fa;
          padding: 16px 50px 10px;
          h4 {
            color: #cb3534;
            font-weight: normal;
            font-size: 10px;
            span {
              display: inline-block;
              width: 12px;
              height: 12px;
              background: #cb3534;
              text-align: center;
              line-height: 12px;
              color: #fff;
              border-radius: 50%;
              font-size: 8px;
              margin-right: 12px;
            }
          }
          p {
            font-size: 10px;
            padding-left: 24px;
            margin: 5px 0;
            span {
              letter-spacing: 4px;
            }
          }
        }
        .g-btn {
          text-align: center;
          margin-top: 52px;
          button {
            width: 160px;
            height: 40px;
            margin: 0 auto;
            padding: 0;
            background: #679fec;
            border: 1px solid #679fec;
            border-radius: 0;
            font-size: 14px;
          }
        }
      }
    }
  }
}
</style>
