<template>
  <div class="reset">
    <div class="resetMain">
      <Row>
        <Col span="24" class="g-return">
        <Icon type="chevron-left"></Icon>
        <a @click='returnHome'>返回首页</a>
        </Col>
      </Row>
      <div class="container">
        <Tabs :value="current">
          <TabPane label="1.选择账号" name="first">
            <div class="g-box">
              <Row class="g-top22">
                <Col span="3" class="g-right">输入邮箱</Col>
                <Col span="11">
                <Input v-model="email" style="width: 260px"></Input>
                </Col>
              </Row>
              <Row class="g-top22">
                <Col span="3" class="g-right">输入验证码</Col>
                <Col span="11">
                <Input v-model="checkCode" style="width: 260px"></Input>
                </Col>
                <Col span="8">
                <span class="checkImg">
                  <p class="code">{{randomCode}} </p>
                </span>
                <a class="g-changImg" @click="createCode">看不清，换一张</a>
                </Col>
              </Row>
              <Row>
                <Col span="20">
                <div class="code-row-top">
                  <Button type="success" long @click="resetPassword">找回密码</Button>
                </div>
                </Col>
              </Row>
            </div>
          </TabPane>
          <TabPane label="2.验证安全邮箱" name="second">
            <div class="g-emailBox">
              <img :src="imgUrl">
              <Row>
                <Col span="24" class="g373d41"> 验证码已发送到您的邮箱
                <span>{{ email }}</span>邮箱！
                </Col>
              </Row>
              <Row>
                <Col span="24" class="g679feb">
                <a @click="linkTo"> 请在30分钟内点击链接重置密码</a>
                </Col>
              </Row>
              <Row>
                <Col span="24">
                <Button class="NextBtn">下一步</Button>
                </Col>
              </Row>
            </div>
          </TabPane>
        </Tabs>
      </div>
    </div>
    <footer-tag></footer-tag>
  </div>
</template>

<script>
import FooterTag from "components/Footer";
import { reset } from "api/login";
export default {
  data() {
    return {
      current: "first",
      email: "",
      randomCode: "",
      checkCode: "",
      imgUrl: "../../../static/img/wait2.png"
    };
  },
  components: {
    FooterTag
  },
  created() {
    this.createCode();
  },
  methods: {
    linkTo() {
      this.$router.push({ path: "/resetpassword" });
    },
    // 重置密码
    resetPassword() {
      if (this.checkCode !== this.randomCode) {
        this.$Message.error("验证码错误");
      } else if (this.email === "") {
        this.$Message.error("请输入邮箱");
      } else {
        reset({ email: this.email })
          .then(r => {
            if (r.data.success) {
              this.current = "second";
            }
          })
          .catch(e => {
            if (e) {
              this.$Message.error(e.message);
            }
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
  .container {
    width: 600px;
    margin: 15px auto 0;
    .ivu-tabs-bar {
      border-bottom: 2px solid #679feb;
    }
    .ivu-tabs-ink-bar {
      background: none;
    }
    .ivu-tabs-tab {
      color: #808080;
      font-size: 16px;
      padding: 17px 104px;
      margin: 0;
    }
    .ivu-tabs-nav .ivu-tabs-tab-active {
      color: #679fea;
      background: url(img/sanjiao.png) no-repeat 135px 43px;
    }
    .ivu-tabs-bar{
      .ivu-tabs-nav-next,.ivu-tabs-nav-prev{
        display: none
      }
    }
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
