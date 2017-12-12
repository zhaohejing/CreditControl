<template>
    <div class="reset">
        <div class="resetMain">
            <Row>
                <Col span="24" class="g-return">
                <Icon type="chevron-left"></Icon>
                <a @click='returnHome'>返回首页</a>
                </Col>
            </Row>
            <div class="container g-border">
                <div class="g-box">
                    <Row class="g-top22">
                        <Col span="7" class="g-right">输入邮箱</Col>
                        <Col span="11">
                        <Input v-model="email" placeholder="输入邮箱" style="width: 260px"></Input>
                        </Col>
                    </Row>
                    <Row class="g-top22">
                        <Col span="7" class="g-right">输入重置码</Col>
                        <Col span="11">
                        <Input v-model="code" placeholder="输入重置码" style="width: 260px"></Input>
                        </Col>
                    </Row>
                    <Row class="g-top22">
                        <Col span="7" class="g-right">输入新密码</Col>
                        <Col span="11">
                        <Input type="password" placeholder="输入新密码" v-model="password" style="width: 260px"></Input>
                        </Col>
                    </Row>
                    <Row>
                        <Col span="24">
                        <div class="code-row-top">
                            <Button type="success" long @click="resetPassword">提交修改</Button>
                        </div>
                        </Col>
                    </Row>
                </div>
            </div>
        </div>
        <footer-tag></footer-tag>
    </div>
</template>

<script>
import FooterTag from "components/Footer";
import { password } from "api/login";
export default {
  data() {
    return {
      email: "",
      password: "",
      code: "",
      imgUrl: "../../../static/img/wait2.png"
    };
  },
  components: {
    FooterTag
  },
  methods: {
    // 重置密码
    resetPassword() {
      if (this.email === "") {
        this.$Message.error("请输入邮箱");
      } else if (this.code === "") {
        this.$Message.error("请输入重置码");
      } else if (this.password === "") {
        this.$Message.error("请输入新密码");
      } else {
        const model = {
          email: this.email,
          code: this.code,
          password: this.password
        };
        password(model)
          .then(r => {
            if (r.data.success) {
              this.$Message.success("密码重置成功");
              this.$router.push({ path: "/login" });
            }
          })
          .catch(e => {
            this.$$Message.error(e.message);
          });
      }
    },

    // 返回首页
    returnHome() {
      this.$router.push({ path: "/login" });
    },
    // 图片验证码
    createCode() {
      let code = "";
      const random = [0, 1, 2, 3, 4, 5, 6, 7, 8, 9];
      for (let i = 0; i < 4; i++) {
        const index = Math.floor(Math.random() * 9);
        code += random[index];
      }
      this.randomCode = code;
    }
  }
};
</script>

<style rel="stylesheet/scss" lang="scss">
div {
  font-family: "Microsoft Yahei", sans-serif;
}

.reset {
  height: 100%;
}

.resetMain {
  height: calc(100% - 42px);
  padding: 79px 98px;
  .g-return {
    a {
      color: #383d41;
      font-size: 16px;
      &:hover {
        cursor: pointer;
      }
    }
  }
  .g-border {
    border: 1px solid #ebebeb;
  }
  .container {
    width: 600px;
    margin: 15px auto 0;
    text-align: center;
    padding-bottom: 79px;
    .g-box {
      margin-top: 84px;
      .ivu-input {
        height: 38px;
        line-height: 38px;
        border-radius: 0;
        color: #4d4d4d;
        font-size: 14px;
        width: 258px;
      }
      .code-row-top {
        width: 160px;
        margin: 60px auto 0;
        button {
          width: 160px;
          height: 40px;
          background: #679fec;
          border: none;
          border-radius: 0;
          font-size: 14px;
        }
      }
    }
    .g-right {
      text-align: right;
      margin-right: 28px;
      height: 38px;
      line-height: 38px;
      font-size: 14px;
      color: #9b9ea0;
    }
    .g-top22 {
      margin-top: 22px;
      .checkImg {
        width: 95px;
        height: 38px;
        margin-left: -10px;
        display: inline-block;
        background: #ebc5ec;
      }
      .g-changImg {
        line-height: 38px;
        float: right;
        color: #d47b7b;
        text-decoration: underline;
        font-size: 14px;
        &:hover {
          cursor: pointer;
        }
      }
    }
    .g-emailBox {
      width: 380px;
      margin: 54px auto 0;
      text-align: center;
      font-size: 16px;
      img {
        margin-bottom: 54px;
      }
      .g373d41 {
        color: #373d41;
        text-align: left;
        span {
          color: #b84a4a;
        }
      }
      .g679feb {
        text-align: left;
        color: #679feb;
        padding-top: 30px;
        margin-bottom: 68px;
      }
      .NextBtn {
        width: 160px;
        height: 40px;
        background: #679fec;
        border: none;
        border-radius: 0;
        font-size: 14px;
        color: #fff;
      }
    }
    .code {
      line-height: 38px;
      float: left;
      margin-left: 28px;
      color: blue;
      text-decoration: underline;
      font-size: 20px;
    }
  }
}
</style>
