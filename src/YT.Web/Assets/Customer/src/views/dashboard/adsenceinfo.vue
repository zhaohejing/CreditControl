<template>
  <div class='info'>
    <div class='infoMain'>
      <Row class='infoTitle'>
        <Col span='24' class='g-center'>{{info.title}}</Col>
      </Row>
      <hr style="height:5px;border:none;border-top:5px groove"/>
      <Row>
        <Col span='24' class='g-time'>{{info.creationTime| formatDate}}</Col>
      </Row>
      <Row class='infoTitle'>
        <Col span='24' class='g-center'>
        <div v-html="info.content"></div>
        </Col>
      </Row>
      <BackTop> </BackTop>
    </div>
  </div>
</template>

<script>
import { info } from "api/public";
import { parseTime } from "utils/index";
export default {
  data() {
    return {
      info: {}
    };
  },
  components: {},
  // 过滤器
  filters: {
    formatDate(time) {
      return parseTime(time);
    }
  },
  created() {
    this.Init();
  },
  methods: {
    Init() {
      const id = this.$route.params.id;
      info({
        id: id
      })
        .then(r => {
          if (r.data.success) {
            this.info = r.data.result;
          }
        })
        .catch(e => {
          this.$Message.error(e.response.data.error.message);
        });
    }
  }
};
</script>

<style rel='stylesheet/scss' lang='scss'>
div {
  font-family: "Microsoft Yahei";
}

.g-center {
  text-align: center;
}

.g-time {
  text-align: center;
  font-family: "Microsoft Yahei";
  font-size: 10px;
}

.info {
  .infoMain {
    background: #f5f5f6;
    padding-bottom: 35px;
    overflow: auto;
    .ivu-form-item-required .ivu-form-item-label:before {
      display: none;
    }
    .infoTitle {
      padding-top: 40px;
      font-size: 20px;
      color: #373d41;
    }
    .infoBox {
      width: 1000px;
      margin: 0 auto;
      background: #fff;
      padding: 58px 96px;
      border-bottom: 2px solid #ebebeb;
      .basic {
        font-size: 20px;
        color: #373d41;
        margin-bottom: 46px;
      }
      .infoInput {
        width: 490px;
        margin: 0 auto;
        label {
          color: #9b9ea0;
          font-size: 14px;
        }
        input {
          height: 40px;
          border-radius: 0;
        }
      }
      .c9 {
        color: #9b9ea0;
        font-size: 14px;
        padding-top: 35px;
        margin-bottom: 8px;
      }
    }
    .infoTextArea {
      textarea.ivu-input {
        padding: 12px;
      }
    }
    .submitButton {
      margin: 120px 0 62px;
      button {
        width: 200px;
        height: 40px;
        background: #679fec;
        padding: 0;
        border: 1px solid #679fec;
        border-radius: 0;
        font-size: 14px;
      }
    }
    .fileUpload {
      width: 645px;
      margin: 0 auto;
      img {
        width: 160px;
        height: 80px;
        border: 1px solid #c4c5c7;
      }
      .fileBox {
        width: 160px;
        height: 80px;
        line-height: 98px;
      }
      .ivu-upload-drag {
        margin: 0 auto;
        width: 160px;
        height: 80px;
        line-height: 98px;
        border: 1px solid #c4c5c7;
        border-radius: 0;
      }
      .g9b9ea0 {
        color: #9b9ea0;
        font-size: 14px;
      }
      .g-marginLeft {
        margin: 0 143px 0 36px;
      }
    }
  }
}

.demo-upload-list {
  display: inline-block;
  width: 60px;
  height: 60px;
  text-align: center;
  line-height: 60px;
  border: 1px solid transparent;
  border-radius: 4px;
  overflow: hidden;
  background: #fff;
  position: relative;
  box-shadow: 0 1px 1px rgba(0, 0, 0, 0.2);
  margin-right: 4px;
}

.demo-upload-list img {
  width: 100%;
  height: 100%;
}

.demo-upload-list-cover {
  display: none;
  position: absolute;
  top: 0;
  bottom: 0;
  left: 0;
  right: 0;
  background: rgba(0, 0, 0, 0.6);
}

.demo-upload-list:hover .demo-upload-list-cover {
  display: block;
}

.demo-upload-list-cover i {
  color: #fff;
  font-size: 20px;
  cursor: pointer;
  margin: 0 2px;
}
</style>
