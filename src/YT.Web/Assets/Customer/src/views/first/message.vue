<template>
  <div class="messageSuccess">
    <div class="headMain">
      <div class="headBox">
        <div class="g-box">
          <img :src="imgUrl">
          <Row class="g-left g-pt40">
            <col span="24">
            <font>您的资料已成功提交</font>，我们会在24小时内审核您的需求！
            </col>
          </Row>
          <Row class="g-left">
            <col span="24"> 如有疑问请联系010-56261409
            </col>
          </Row>
          <Row class="returnBtn">
            <col span="24">
            <Button long @click="returnHome">返回首页</Button>
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
export default {
  data() {
    return {
      imgUrl: "../../../static/img/wait2.png"
    };
  },
  components: {
    FooterTag,
    HeadTopTag
  },
  methods: {
    returnHome() {
      this.$router.push({ path: "/login" });
    }
  }
};
</script>
<style rel="stylesheet/scss" lang="scss" scoped>
.messageSuccess {
  height: 100%;
  .headMain {
    background: #f5f5f6;
    padding: 86px 0;
    .headBox {
      width: 996px;
      height: 530px;
      margin: 0 auto;
      background: #fff;
      .g-box {
        width: 520px;
        margin: 0 auto;
        text-align: center;
        padding-top: 70px;
        font-size: 20px;
        font {
          color: #b84a4a;
        }
        .g-left {
          text-align: left;
        }
        .g-pt40 {
          padding-top: 40px;
          padding-bottom: 26px;
        }
        .returnBtn {
          margin-top: 65px;
          button {
            width: 320px;
            height: 40px;
            background: #679fec;
            border: #679fec;
            border-radius: 0;
            color: #fff;
            font-size: 16px;
          }
        }
      }
    }
  }
}
</style>