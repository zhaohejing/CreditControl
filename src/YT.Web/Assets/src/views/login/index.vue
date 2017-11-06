
<template>
  <div class="login-main">
    <div class="login-container">
      <Row class="g-center">
        <col span="24">
        <img :src="logo" style="width:180px;height:90px">
        </col>
      </Row>
      <Row class="g-center g-top22">
        <col span="24"> 北京视界奇点文化传媒有限公司
        <br/> 后台管理平台
        </col>
      </Row>
      <transition name="slide-fade">
        <Form v-if="showLogin" ref="loginForm" autoComplete="on" :model="loginForm" :rules="loginRules" class="card-box login-form">
          <Form-item prop="user">
            <Input type="text" v-model="loginForm.usernameOrEmailAddress" placeholder="用户名" autoComplete="on">
            </Input>
          </Form-item>
          <Form-item prop="password">
            <Input type="password" v-model="loginForm.password" placeholder="密码" @keyup.enter.native="handleLogin">
            </Input>
          </Form-item>
          <Form-item style="margin-bottom:-2px">
            <Button type="primary" @click="login" long>登录</Button>
          </Form-item>
         
        </Form>
      </transition>
    </div>
    <footer-tag></footer-tag>
  </div>
</template>

<script>
import { Authenticate } from 'api/login'
import FooterTag from 'components/Footer'
export default {
  name: 'login',
  components: {
    FooterTag
  },
  data() {
    return {
      logo: '../../../static/img/logo.png',
      loginForm: {
        usernameOrEmailAddress: 'admin',
        password: '123456'
      },
      loginRules: {
        usernameOrEmailAddress: [
          { required: true, trigger: 'blur' }
        ],
        password: [
          { required: true, trigger: 'blur' }
        ]
      },
      loading: false,
      showDialog: false,
      showLogin: false
    }
  },
  mounted() {
    this.showLogin = true
  },
  methods: {
    // 登录
    login() {
      this.$refs.loginForm.validate(valid => {
        if (valid) {
          this.loading = true;
          this.$store.dispatch('LoginByEmail', this.loginForm).then(() => {
            this.$Message.success('登录成功');
            this.loading = false;
            this.$router.push({ path: '/' });
          }).catch(err => {
            this.$Message.error(err.response.data.error.details);
            this.loading = false;
          });
        } else {
          return false;
        }
      });
    }
  }
}
</script>

<style rel="stylesheet/scss" lang="scss">
.login-main {
  height: 100%;
}

.g-center {
  text-align: center
}

.login-container {
  background: url('./img/bg.jpg');
  margin: 0px;
  overflow: hidden;
  height: calc(100% - 42px);
  background-size: cover;
  padding-top: 80px;
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
    background: url('../../../static/img/login01.png');
    border-radius: 8px;
    button {
      margin-top: 27px;
      height: 40px;
      background: #679feB;
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
      text-align: right
    }
  }
}

.slide-fade-enter-active {
  transition: all .3s ease;
}

.slide-fade-leave-active {
  transition: all .8s cubic-bezier(0.68, -0.55, 0.27, 1.55);
}

.slide-fade-enter,
.slide-fade-leave-to {
  transform: translate3d(0, -50px, 0);
  opacity: 0;
}
</style>
