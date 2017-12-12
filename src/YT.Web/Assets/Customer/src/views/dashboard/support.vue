<template>
    <div class="support">
        <Row class="supportMain">
            <div class="rowBox">
                <img src="../../../static/img/support.png" alt="">
                <p>感谢您对信用华夏的支持，我们会尽快确认您的充值金额！</p>
                <p>如有疑问请联系010-56261409</p>
                <Button type="primary" @click="returnHome">返回首页</Button>
            </div>
        </Row>
        <footer-tag></footer-tag>
    </div>
</template>

<script>
import FooterTag from "components/Footer";
export default {
  data() {
    return {};
  },
  components: {
    FooterTag
  },
  methods: {
    returnHome() {
      this.$router.push({ path: "/dashboard" });
    }
  }
};
</script>

<style rel="stylesheet/scss" lang="scss" scoped>
.support {
  .g-center {
    text-align: center;
  }
  height: 100%;
  .supportMain {
    padding: 86px 105px;
    background: #f5f5f7;
    .rowBox {
      background: #fff;
      text-align: center;
      padding: 72px 0 204px;
      img {
        margin-bottom: 35px;
      }
      p {
        font-size: 16px;
        width: 418px;
        text-align: left;
        margin: 0 auto;
        padding-top: 18px;
      }
      button {
        width: 160px;
        height: 40px;
        text-align: center;
        line-height: 40px;
        background: #679fec;
        border: 1px solid #679fec;
        font-size: 14px;
        padding: 0;
        border-radius: 0;
        margin-top: 107px;
      }
    }
  }
}
</style>
