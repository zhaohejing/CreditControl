<template>
  <div class='info'>
    <head-top-tag></head-top-tag>
    <div class='infoMain'>
      <Row class='infoTitle'>
        <Col span='24' class='g-center'>客户资料信息</Col>
      </Row>
      <Form ref='form' :model='form' :rules='ruleValidate' label-position="top">
        <div class="infoBox">
          <Row class='basic'>
            <Col span='24'>客户基本信息</Col>
          </Row>
          <div class="infoInput">
            <FormItem label="公司名称" prop='companyName'>
              <Input v-model="form.companyName"></Input>
            </FormItem>
            <FormItem label="所属行业">
              <Input v-model="form.industry"></Input>
            </FormItem>
            <FormItem label="品牌名称">
              <Input v-model="form.brands"></Input>
            </FormItem>
            <FormItem label="法人代表">
              <Input v-model="form.legalPerson"></Input>
            </FormItem>
            <FormItem label="法人代表手机号码">
              <Input v-model="form.legalMobile"></Input>
            </FormItem>
            <FormItem label="品牌负责人">
              <Input v-model="form.brandsPerson"></Input>
            </FormItem>
            <FormItem label="品牌负责人手机号码">
              <Input v-model="form.brandsMobile"></Input>
            </FormItem>
            <FormItem label="联系地址">
              <Input v-model="form.address"></Input>
            </FormItem>
            <FormItem label="邮编">
              <Input v-model="form.postNum"></Input>
            </FormItem>
            <FormItem label="电子邮箱" prop='email'>
              <Input v-model="form.email"></Input>
            </FormItem>
          </div>
        </div>
        <div class="infoBox">
          <Row class='basic'>
            <Col span='24'>资料上传</Col>
          </Row>
          <Row class='fileUpload'>
            <Row>
              <Col span="6"><h3>企业营业执照副本</h3></Col>
              <Col span="9">
              <template class="demo-upload-list" v-if="form.license">
                <img :src="form.licenseUrl">
                <div class="demo-upload-list-cover">
                  <Icon type="ios-trash-outline" @click.native="removeLicense(form.license)"></Icon>
                </div>
              </template>
              <Upload v-else type='drag' :on-error="licenseerror" :on-success="licensesuccess" :headers="upload.headers" :action="upload.url">
                <div class='fileBox'>
                  <Icon type='ios-plus-empty' size='38' style='color: #d3d3d3'></Icon>
                </div>
              </Upload>
              <p class='g9b9ea0 g-center'>企业有效期营业执照复本电子版上传</p>
              </Col>
            </Row>
            <Row>
              <Col span="6"><h3>企业法人身份证</h3></Col>
              <Col span="9">
              <template class="demo-upload-list" v-if="form.license">
                <img :src="form.licenseUrl">
                <div class="demo-upload-list-cover">
                  <Icon type="ios-trash-outline" @click.native="removeLicense(form.license)"></Icon>
                </div>
              </template>
              <Upload v-else type='drag' :on-error="licenseerror" :on-success="licensesuccess" :headers="upload.headers" :action="upload.url">
                <div class='fileBox'>
                  <Icon type='ios-plus-empty' size='38' style='color: #d3d3d3'></Icon>
                </div>
              </Upload>
              <p class='g9b9ea0 g-center'>身份证正面</p>
              </Col>
              <Col span="9" >
              <template class="demo-upload-list" v-if="form.permitCard">
                <img :src="form.permitCardUrl">
                <div class="demo-upload-list-cover">
                  <Icon type="ios-trash-outline" @click.native="removePermitCard(form.permitCard)"></Icon>
                </div>
              </template>
              <Upload v-else type='drag' :on-error="permiterror" :on-success="permitsuccess" :headers="upload.headers" :action="upload.url">
                <div class='fileBox'>
                  <Icon type='ios-plus-empty' size='38' style='color: #d3d3d3'></Icon>
                </div>
              </Upload>
              <p class='g9b9ea0 g-center'>身份证反面</p>
              </Col>
            </Row>
            <Row>
                <Col span="12"><h3>企业所属行业特有许可证或企业荣誉</h3></Col>
                <Col span="12">
                     <template class="demo-upload-list" v-if="form.license">
                <img :src="form.licenseUrl">
                <div class="demo-upload-list-cover">
                  <Icon type="ios-trash-outline" @click.native="removeLicense(form.license)"></Icon>
                </div>
              </template>
              <Upload v-else type='drag' :on-error="licenseerror" :on-success="licensesuccess" :headers="upload.headers" :action="upload.url">
                <div class='fileBox'>
                  <Icon type='ios-plus-empty' size='38' style='color: #d3d3d3'></Icon>
                </div>
              </Upload>
              <p class='g9b9ea0 g-center'>企业所属行业特有许可证或企业荣誉</p>
                </Col>
            </Row>
            <Row>
              <enlargeimg :data="files"></enlargeimg>
            </Row>
          </Row>
        </div>

        <div class="infoBox">
          <Row class='basic'>
            <Col span='24'>相关资料</Col>
          </Row>
          <Row class='fileUpload'>
            <Row>
                <Col span="12"><h3>相关业务资料</h3></Col>
              <Col span="12">
              <template class="demo-upload-list" v-if="form.license">
                <img :src="form.licenseUrl">
                <div class="demo-upload-list-cover">
                  <Icon type="ios-trash-outline" @click.native="removeLicense(form.license)"></Icon>
                </div>
              </template>
              <Upload v-else type='drag' :on-error="licenseerror" :on-success="licensesuccess" :headers="upload.headers" :action="upload.url">
                <div class='fileBox'>
                  <Icon type='ios-plus-empty' size='38' style='color: #d3d3d3'></Icon>
                </div>
              </Upload>
              <p class='g9b9ea0 g-center'>企业有效期营业执照复本电子版上传</p>
              </Col>
            </Row>

          </Row>
        </div>

        <Row class="g-center submitButton">
          <Button @click="submit" type="primary">提交并结算</Button>
        </Row>
        <BackTop> </BackTop>
      </Form>
    </div>

    <footer-tag></footer-tag>
  </div>
</template>

<script>
import HeadTopTag from "components/HeadTop";
import FooterTag from "components/Footer";
import { getFormByOrder, modifyForm } from "api/product";
import enlargeimg from "enlargeimg";
export default {
  data() {
    return {
      form: {},
      upload: {
        url: "http://47.93.2.82:9999/api/File/ImageUpload",
        // headers: { 'Content-Type': 'multipart/form-data' }
        headers: {}
      },
      files: [
        {
          path:
            "http://tupian.enterdesk.com/uploadfile/2013/1219/20131219053302481.jpg"
        },
        {
          path:
            "http://tupian.enterdesk.com/uploadfile/2013/1219/20131219053302481.jpg"
        },
        {
          path:
            "http://tupian.enterdesk.com/uploadfile/2013/1219/20131219053302481.jpg"
        }
      ],
      ruleValidate: {
        companyName: [
          {
            required: true,
            message: "公司名称不能为空",
            trigger: "blur"
          }
        ],
        email: [
          {
            type: "email",
            message: "邮箱格式不正确",
            trigger: "blur"
          }
        ]
      }
    };
  },
  components: {
    FooterTag,
    HeadTopTag,
    enlargeimg
  },
  created() {
    this.Init();
  },
  activated() {
    this.Init();
  },
  methods: {
    Init() {
      const order = this.$route.query.orderId;
      getFormByOrder({
        id: order
      })
        .then(r => {
          if (r.data.success) {
            this.form = r.data.result;
            this.form.orderId = this.$route.query.id;
          }
        })
        .catch(e => {
          this.$Message.error(e.response.data.error.message);
        });
    },
    submit() {
      this.form.orderId = this.$route.query.orderId;
      modifyForm(this.form).then(r => {
        if (r.data.success) {
          this.$router.push({
            path: "/order"
          });
        }
      });
    },
    removeLicense(guid) {
      this.form.license = "";
      this.form.licenseUrl = "";
    },
    removeIdentityCard(guid) {
      this.form.identityCard = "";
      this.form.identityCardUrl = "";
    },
    removeLogo(guid) {
      this.form.companyLogo = "";
      this.form.companyLogoUrl = "";
    },
    removePermitCard(guid) {
      this.form.permitCard = "";
      this.form.permitCardUrl = "";
    },
    licenseerror(error, file) {
      if (!file.success) {
        this.$Message.error(file.error.message);
      }
    },
    licensesuccess(response, file) {
      if (response.success) {
        this.form.license = response.result[0].guid;
        this.form.licenseUrl = response.result[0].viewUrl;
      }
    },
    carderror(error, file) {
      if (!file.success) {
        this.$Message.error(file.error.message);
      }
    },
    cardsuccess(response, file) {
      if (response.success) {
        this.form.identityCard = response.result[0].guid;
        this.form.identityCardUrl = response.result[0].viewUrl;
      }
    },
    logoerror(error, file) {
      if (!file.success) {
        this.$Message.error(file.error.message);
      }
    },
    logosuccess(response, file) {
      if (response.success) {
        this.form.companyLogo = response.result[0].guid;
        this.form.companyLogoUrl = response.result[0].viewUrl;
      }
    },
    permiterror(error, file) {
      if (!file.success) {
        this.$Message.error(file.error.message);
      }
    },
    permitsuccess(response, file) {
      if (response.success) {
        this.form.permitCard = response.result[0].guid;
        this.form.permitCardUrl = response.result[0].viewUrl;
      }
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

.info {
  .infoMain {
    background: #f5f5f6;
    padding-bottom: 35px;
    overflow: auto;
    .ivu-form-item-required .ivu-form-item-label:before {
      display: none;
    }
    .infoTitle {
      padding-top: 80px;
      padding-bottom: 40px;
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
      img {
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
