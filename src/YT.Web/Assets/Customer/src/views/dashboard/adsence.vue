<template>
    <div class="setting">
        <Row class="settingMain">
            <div class="settingTop">
                <col span="24">
                <img
                 :src="imgUrl"
                style="width:87px;height:42px">
                </col>
                <div class="g-center g-text">
                    尊敬的合作伙伴，信用华夏很高兴能为您服务，如果您遇到任何问题<br/> 请致电客服服务或发邮件
                </div>
            </div>
            <div class="settingBottom">
                <div class="g-center g-text g-bcolor">
                    <p>
                        <img :src="telIcon"> {{ tel }}
                    </p>
                    <p class="padding-top">
                        <img :src="emailIcon"> {{ email }}
                    </p>
                </div>
            </div>
        </Row>
    </div>
</template>

<script>
export default {
  data() {
    return {
      imgUrl: "../../../static/img/logo.png",
      telIcon: "../../../static/img/phone.png",
      emailIcon: "../../../static/img/email.png",
      tel: "010-56261409",
      email: "hezuo@bjsjqd.cn"
    };
  }
};
</script>

<style rel="stylesheet/scss" lang="scss" scoped>
.g-center {
  text-align: center;
}

.setting {
  width: 100%;
  padding: 80px 68px;
  .settingMain {
    width: 100%;
    box-shadow: 0px 0px 7px 4px rgba(1, 34, 63, 0.1);
    .settingTop {
      height: 160px;
      background: #f7f4ed;
      padding: 15px 25px 5px;
    }
    .g-text {
      width: 542px;
      margin: 0 auto;
      text-align: left;
      font-size: 18px;
      color: #bfae84;
      line-height: 30px;
    }
    .settingBottom {
      width: 100%;
      padding: 34px 0 202px;
      .g-bcolor {
        color: #373d41;
        font-family: "arial-Regular";
        font-size: 16px;
      }
      img {
        vertical-align: middle;
        padding-right: 12px;
      }
      .padding-top {
        padding-top: 24px;
      }
    }
  }
}
</style>
