<template>
  <div class='register'>
    <head-top-tag></head-top-tag>
    <div class='regMain'>
      <Row class='regTitle'>
        <Col span='24' class='g-center'>用户设置</Col>
      </Row>
      <div class='regBox'>
        <Form ref='formValidate' :model='formValidate' :rules='ruleValidate' :label-width='170'>
          <Row class='g-padding70 g-pb9'>
            <Col span='24'>公司基本信息</Col>
          </Row>
          <FormItem label='公司名称' class='g-rowTop'>
            <Row>
              <Col span='11'>
              <FormItem prop='companyName'>
                <Input :disabled="isDisabled" type='text' v-model='formValidate.companyName'></Input>
              </FormItem>
              </Col>
              <Col span="1" offset="2">
              <Icon  v-if="formValidate.companyName" color="green" type="checkmark-circled"></Icon>
              </Col>
            </Row>
          </FormItem>
          <FormItem label='选择省份' class='g-rowTop'> 
            <Row>
              <Col span='11'>
              <FormItem prop='prov' >
              	<Select :disabled="isDisabled" @on-change="changeProv"  v-model="formValidate.provence">
					<Option v-for="option in proves" :value="option.name" :key="option.name">
						{{ option.name }}
					</Option>
				</Select >
              </FormItem>
              </Col>
            </Row>
          </FormItem>
          <FormItem label='选择城市' class='g-rowTop'>
            <Row>
              <Col span='11'>
              <FormItem prop='city'>
                <Select :disabled="isDisabled" v-model="formValidate.city">
                  <Option v-for="option in citys" :value="option.name" :key="option.name">
                    {{ option.name }}
                  </Option>
                </Select>
              </FormItem>
              </Col>
            </Row>
          </FormItem>
		     
          <FormItem label='公司地址' class='g-rowTop'>
            <Row>
              <Col span='11'>
              <FormItem prop='address'>
                <Input :disabled="isDisabled" type='text' v-model='formValidate.address'></Input>
              </FormItem>
              </Col>
            </Row>
          </FormItem>
          <Row class='g-padding70 g-pb9'>
            <Col span='24'>联系人基本信息</Col>
          </Row>
          <FormItem label='联系人姓名' class='g-rowTop'>
            <Row>
              <Col span='11'>
              <FormItem prop='contact'>
                <Input :disabled="isDisabled" type='text' v-model='formValidate.contact'></Input>
              </FormItem>
              </Col>
              <Col span="1" offset="2">
              <Icon v-if="formValidate.contact" color="green" type="checkmark-circled"></Icon>
              </Col>
            </Row>
          </FormItem>
          <FormItem label='手机号码' class='g-rowTop'>
            <Row>
              <Col span='11'>
              <FormItem prop='mobile'>
                <Input :disabled="isDisabled" type='text' number v-model='formValidate.mobile'></Input>
              </FormItem>
              </Col>
              <Col span="1" offset="2">
              <Icon v-if="formValidate.mobile" color="green" type="checkmark-circled"></Icon>
              </Col>
            </Row>
          </FormItem>
          <FormItem label='联系邮箱' class='g-rowTop'>
            <Row>
              <Col span='11'>
              <FormItem prop='email'>
                <Input :disabled="isDisabled" type='text' v-model='formValidate.email'></Input>
              </FormItem>
              </Col>
              <Col span="1" offset="2">
              <Icon v-if="formValidate.email" color="green" type="checkmark-circled"></Icon>
              </Col>
            </Row>
          </FormItem>
          <FormItem label='登录用户名' class='g-rowTop'>
            <Row>
              <Col span='11'>
              <FormItem prop='account'>
                <Input :disabled="isDisabled" type='text' v-model='formValidate.account'></Input>
              </FormItem>
              </Col>
              <Col span="1" offset="2">
              <Icon v-if="formValidate.account" color="green" type="checkmark-circled"></Icon>
              </Col>
            </Row>
          </FormItem>
          <FormItem label='设置密码' class='g-rowTop'>
            <Row>
              <Col span='11'>
              <FormItem prop='password'>
                <Input :disabled="isDisabled" type='password' v-model='formValidate.password'></Input>
              </FormItem>
              </Col>
              <Col span="1" offset="2">
              <Icon v-if="formValidate.repeatPassword&&formValidate.password&&formValidate.password==formValidate.repeatPassword" color="green" type="checkmark-circled"></Icon>
              </Col>
            </Row>
          </FormItem>
          <FormItem label='再次输入设置密码' class='g-rowTop'>
            <Row>
              <Col span='11'>
              <FormItem prop='repeatPassword'>
                <Input :disabled="isDisabled" type='password' v-model='formValidate.repeatPassword'></Input>
              </FormItem>
              </Col>
              <Col span="1" offset="2">
              <Icon v-if="formValidate.repeatPassword&&formValidate.password&&formValidate.password==formValidate.repeatPassword" color="green" type="checkmark-circled"></Icon>
              </Col>
            </Row>
          </FormItem>
          <Row class='g-padding70 g-pb9'>
            <Col span='24'>公司资质信息</Col>
          </Row>
          <Row class='fileUpload'>
            <Col span='24'>
            <Col span='5' class='g-center g-marginLeft'>
            <template class="demo-upload-list" v-if="formValidate.license">
              <img :src="formValidate.licenseUrl">
              <div class="demo-upload-list-cover">
                <Icon type="ios-trash-outline" @click.native="removeLicense(formValidate.license)"></Icon>
              </div>
            </template>
            <Upload v-else type='drag' :on-error="licenseerror" :on-success="licensesuccess" :headers="upload.headers" :action="upload.url">
              <div class='fileBox'>
                <Icon type='ios-plus-empty' size='38' style='color: #d3d3d3'></Icon>
              </div>
            </Upload>
            <p class='g9b9ea0'>上传公司营业执照副本</p>
            <!-- <p v-if="!formValidate.license" class='g9b9ea0'>上传公司营业执照副本</p> -->
            </Col>

            <Col span='5' class='g-center'>
            <template class="demo-upload-list" v-if="formValidate.identityCard">
              <img :src="formValidate.identityCardUrl">
              <div class="demo-upload-list-cover">
                <Icon type="ios-trash-outline" @click.native="removeIdentityCard(formValidate.identityCard)"></Icon>
              </div>
            </template>
            <Upload v-else type='drag' :on-error="carderror" :on-success="cardsuccess" :headers="upload.headers" :action="upload.url">
              <div class='fileBox'>
                <Icon type='ios-plus-empty' size='38' style='color: #d3d3d3'></Icon>
              </div>
            </Upload>
            <p class='g9b9ea0'>上传公司法人身份证照片</p>
            <!-- <p v-if="!formValidate.identityCard" class='g9b9ea0'>上传公司法人身份证照片</p> -->
            </Col>
            </Col>
          </Row>
        </Form>
        <Row class='footbtn'>
          <Col span='8'>
          <Button type='primary' @click="edit">编辑</Button>
          </Col>
          <Col span='8'>
          <Button :disabled="isDisabled"  type='primary' style='margin-left:50px' @click='save'>保存</Button>
          </Col>
        </Row>
      </div>
    </div>
    <footer-tag></footer-tag>
  </div>
</template>

<script>
import HeadTopTag from "components/HeadTop";
import FooterTag from "components/Footer";
import city from "../../../static/city.json";

import { register, info, modify } from "api/login";

export default {
  data() {
    const arrAll = city.arrAll;
    return {
      logoTag: "代理商管理平台",
      imageUrl: "",
      isDisabled: true,
      defaultList: [],
      citys: [],
      proves: arrAll,
      formValidate: {
        companyName: "",
        provence: "",
        city: "",
        address: "",
        contact: "",
        mobile: "",
        email: "",
        account: "",
        password: "",
        repeatPassword: "",
        license: "",
        licenseUrl: "",
        identityCard: "",
        identityCardUrl: "",
        balance: 0,
        state: null
      },
      upload: {
        url: "http://47.93.2.82:9999/api/File/ImageUpload",
        // headers: { 'Content-Type': 'multipart/form-data' }
        headers: {}
      },
      ruleValidate: {
        companyName: [{ required: true, message: "公司名称不能为空", trigger: "blur" }],
        name: [{ required: true, message: "姓名不能为空", trigger: "blur" }],
        account: [{ required: true, message: "账户不能为空", trigger: "blur" }],
        mobile: [
          { required: true, message: "手机不能为空" },
          {
            validator(rule, value, callback, source, options) {
              const errors = [];
              if (!/^1[3|4|5|7|8][0-9]\d{8}$/.test(value)) {
                callback("非法手机号");
              }
              callback(errors);
            }
          }
        ],
        email: [
          { required: true, message: "邮箱不能为空", trigger: "blur" },
          { type: "email", message: "邮箱格式不正确", trigger: "blur" }
        ]
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
      const id = localStorage.getItem("Credit-Id");
      if (!id) this.$router.push({ path: "/login" });
      info({ id: id }).then(r => {
        if (r.data.success) {
          this.formValidate = r.data.result;
        }
      });
    },
    changeProv(current) {
      const temp = this.proves.filter(c => {
        return c.name === current;
      });
      this.citys = temp[0].sub;
    },
    // 返回登录页
    returnPrev() {
      this.$router.push({ path: "/dashboard" });
    },
    save() {
      this.$refs.formValidate.validate(valid => {
        if (valid) {
          modify(this.formValidate).then(r => {
            if (r.data.success) {
              this.$Message.success("保存成功!");
              this.$router.push({ path: "/message" });
            }
          });
        } else {
          this.$Message.error("表单验证失败!");
        }
      });
    },
    edit() {
      this.isDisabled = false;
    },
    removeLicense(guid) {
      this.formValidate.license = "";
      this.formValidate.licenseUrl = "";
    },
    removeIdentityCard(guid) {
      this.formValidate.identityCard = "";
      this.formValidate.identityCardUrl = "";
    },
    licenseerror(error, file) {
      if (!file.success) {
        this.$Message.error(file.error.message);
      }
    },
    licensesuccess(response, file) {
      if (response.success) {
        this.formValidate.license = response.result[0].guid;
        this.formValidate.licenseUrl = response.result[0].viewUrl;
      }
    },
    carderror(error, file) {
      if (!file.success) {
        this.$Message.error(file.error.message);
      }
    },
    cardsuccess(response, file) {
      if (response.success) {
        this.formValidate.identityCard = response.result[0].guid;
        this.formValidate.identityCardUrl = response.result[0].viewUrl;
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
.register {
  .regMain {
    background: #f5f5f6;
    padding-bottom: 35px;
    overflow: auto;
    .regTitle {
      padding-top: 80px;
      padding-bottom: 40px;
      font-size: 20px;
      color: #373d41;
    }
    .regBox {
      width: 996px;
      padding-top: 20px;
      padding-bottom: 78px;
      background: #fff;
      margin: 0 auto;
      padding-left: 226px;
      .g-padding70 {
        padding-top: 70px;
        font-size: 20px;
        color: #373d41;
      }
      .g-pb9 {
        padding-bottom: 9px;
      }
      .ivu-form-item {
        margin-bottom: 0;
      }
      .g-rowTop {
        margin-top: 22px;
        label {
          color: #9b9ea0;
          font-size: 14px;
          margin-right: 17px;
        }
        input {
          height: 40px;
          width: 320px;
          border-radius: 0;
        }
        .ivu-select-single .ivu-select-selection {
          width: 320px;
          height: 40px;
          border-radius: 0;
          line-height: 40px;
          .ivu-select-placeholder {
            line-height: 40px;
          }
        }
        .ivu-select-selected-value {
          line-height: 40px;
        }
        .ivu-select-dropdown {
          width: 320px !important;
        }
      }
    }
    .fileUpload {
      margin-top: 40px;
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
    .footbtn {
      margin-top: 52px;
      button {
        width: 160px;
        height: 40px;
        border: 1px solid #679feb;
        background: #679feb;
        border-radius: 0;
        color: #fff;
        font-size: 14px;
        margin-left: 77px;
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
