<template>
    <div class='login-main'>
        <div class='login-container'>
            <Row  class='g-center'>
              <col span='24'>
                <img :src='logo' style='width:180px;height:108px'>
              </col>  
            </Row>
            <Row  class='g-center g-top22'>
              <col span='24'>
                北京视界奇点文化传媒有限公司<br/>
                代理商管理平台
              </col>  
            </Row>
            <transition name='slide-fade'>
                <Form v-if='showLogin' ref='loginForm' autoComplete='on' :model='loginForm' :rules='loginRules' class='card-box login-form'>
                  <Form-item prop='usernameOrEmailAddress' >
                      <Input type='text' v-model='loginForm.usernameOrEmailAddress' placeholder='用户名' autoComplete='on'>
                      </Input>
                  </Form-item>
                  <Form-item prop='password'>
                      <Input type='password' v-model='loginForm.password' placeholder='密码' @keyup.enter.native='handleLogin'>
                      </Input>
                  </Form-item>
                  <Form-item style='margin-bottom:-2px'>
                      <Button type='primary' @click='login' long>登录</Button>
                  </Form-item>
                  <Row>
                      <Col class='g-tag'  span='12'>
                        <a  @click='reset'>忘记密码？</a>
                      </Col>
                      <Col class='g-tag g-right' span='12'>
                        <a  @click='register'>新用户注册</a>
                      </Col>
                  </Row>
              </Form>
            </transition>
        </div>      
        <footer-tag></footer-tag>
    </div>
</template>

<script>
import { Authenticate } from "api/login";
import FooterTag from "components/Footer";
export default {
  name: "login",
  components: {
    FooterTag
  },
  data() {
    return {
      logo: "../../../static/img/logo.png",
      loginForm: {
        usernameOrEmailAddress: "",
        password: ""
      },
      loginRules: {
        usernameOrEmailAddress: [
          { required: true, message: "用户名不能为空", trigger: "blur" }
        ],
        password: [{ required: true, message: "密码不能为空", trigger: "blur" }]
      },
      loading: false,
      showDialog: false,
      showLogin: false
    };
  },
  mounted() {
    this.showLogin = true;
  },
  methods: {
    // 登录
    login() {
      this.$refs.loginForm.validate(valid => {
        if (valid) {
          Authenticate(
            this.loginForm.usernameOrEmailAddress,
            this.loginForm.password
          )
            .then(response => {
              if (response && response.data && response.data.result) {
                if (response.data.result.state) {
                  this.$Message.success("登录成功");
                  localStorage.setItem("Credit-Id", response.data.result.id);
                  localStorage.setItem(
                    "Credit-Name",
                    response.data.result.companyName
                  );
                  this.$router.push({ path: "/control" });
                } else {
                  this.$router.push({ path: "/message" });
                }
              } else {
                this.$Message.error("登陆失败,请重试");
              }
            })
            .catch(err => {
              this.$Message.error(err.response.data.error.message);
            });
        } else {
          return false;
        }
      });
    },
    // 找回密码
    reset() {
      this.$router.push({ path: "/reset" });
    },
    // 注册
    register() {
      this.$router.push({ path: "/register" });
    }
  }
};
</script>

<style rel='stylesheet/scss' lang='scss'>
.login-main {
  height: 100%;
}
.g-center {
  text-align: center;
}
.login-container {
  background: url(img/bg.jpg);
  margin: 0px;
  overflow: hidden;
  height: calc(100% - 42px);
  background-size: cover;
  padding-top: 50px;
  .g-top22 {
    padding-top: 10px;
    padding-bottom: 10px;
    font-family: "圆通一简";
    font-size: 20px;
    color: #e7c04a;
    text-shadow: 1px 1px 2px#002038;
  }
  input {
    background: rgba(255, 255, 255, 0.5);
    border: 1px solid #fff;
    -webkit-appearance: none;
    border-radius: 0;
    color: #808080;
    height: 40px;
    padding: 4px 18px;
  }
  .login-form {
    width: 440px;
    height: 369px;
    padding: 100px 60px 0px;
    position: absolute;
    left: 50%;
    margin-left: -219px;
    background: url("../../../static/img/login01.png");
    border-radius: 8px;
    button {
      margin-top: 27px;
      height: 40px;
      background: #679feb;
      color: #fff;
      border: 1px solid #679feb;
      border-radius: 0;
      font-size: 14px;
      margin-bottom: 16px;
    }
    .g-tag {
      color: #657fa4;
      &:hover {
        cursor: pointer;
      }
    }
    .g-right {
      text-align: right;
    }
  }
}
.slide-fade-enter-active {
  transition: all 0.3s ease;
}
.slide-fade-leave-active {
  transition: all 0.8s cubic-bezier(0.68, -0.55, 0.27, 1.55);
}
.slide-fade-enter,
.slide-fade-leave-to {
  transform: translate3d(0, -50px, 0);
  opacity: 0;
}
</style>
