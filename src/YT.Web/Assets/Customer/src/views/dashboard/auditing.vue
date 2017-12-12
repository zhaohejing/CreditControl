<template>
  <div class="auditing">
    <head-top-tag></head-top-tag>
    <div class="headMain">
      <div class="headBox">
        <div class="g-box">
          <img :src="imgUrl">
          <Row class="g-left g-pt40">
            <col span="24">
            <font>您的申请提交</font>，我们将会尽快处理您的申请！
            </col>
          </Row>
          <Row class="returnBtn">
            <col span="24">
            <Button long @click="returnPrevPage">关闭并返回上一页</Button>
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
    returnPrevPage() {
      this.$router.push({ path: "/order" });
    }
  }
};
</script>
<style rel="stylesheet/scss" lang="scss" scoped>
.auditing {
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
        .g-pt40 {
          padding-top: 40px;
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