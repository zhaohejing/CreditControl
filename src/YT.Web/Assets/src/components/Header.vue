<template>
  <navbar>
    <button class="navbar-toggler mobile-sidebar-toggler d-lg-none" type="button"
     @click="mobileSidebarToggle">&#9776;</button>
    <a class="navbar-brand"></a>
    <ul class="nav navbar-nav d-md-down-none">
      <li class="nav-item">
        <a class="nav-link navbar-toggler sidebar-toggler" @click="sidebarMinimize">&#9776;</a>
      </li>

    </ul>
    <ul class="nav navbar-nav ml-auto" >
      <li class="nav-item d-md-down-none">
        <a class="nav-link">
          <Icon type="android-notifications" size="20"></Icon>
          <span class="badge badge-pill badge-danger">1</span>
        </a>
      </li>
      <Dropdown class="nav-item">
        <a href="javascript:void(0)">
          <span slot="button">
            <img src="static/img/avatars/6.jpg" class="img-avatar" alt="o">
            <span class="d-md-down-none">{{name}}</span>
          </span>
        </a>
        <Dropdown-menu slot="list">
          <Dropdown-item>
            <p class="dropdown-itemp">
              <Icon type="alert"></Icon>修改个人信息
              <span class="badge badge-info">42</span>
            </p>

          </Dropdown-item>
          <Dropdown-item>
            <p class="dropdown-itemp">
              <Icon type="chatbox-working"></Icon>消息
              <span class="badge badge-success">42</span>
            </p>
          </Dropdown-item>
          <Dropdown-item>
            <p class="dropdown-itemp">
              <Icon type="chatbox-working"></Icon>通知
              <span class="badge badge-danger">42</span>
            </p>
          </Dropdown-item>
          <Dropdown-item divided>
            <p class="dropdown-itemp">
              <Icon type="android-contact"></Icon> 头像</p>
          </Dropdown-item>
          <Dropdown-item>
            <p class="dropdown-itemp">
              <Icon type="android-settings"></Icon> 个人设置</p>
          </Dropdown-item>
          <Dropdown-item>
            <a href="" @click="Logout">
              <p class="dropdown-itemp">
                <Icon type="power"></Icon>登出</p>
            </a>
          </Dropdown-item>
        </Dropdown-menu>
      </Dropdown>
      <li class="nav-item d-md-down-none">
        <a class="nav-link navbar-toggler aside-menu-toggler" @click="asideToggle">&#9776;</a>
      </li>
    </ul>
  </navbar>
</template>
<script>

import navbar from './Navbar'
import { mapGetters, mapActions } from 'vuex';
export default {
  name: 'header',
  components: {
    navbar,
  },
  computed: {
    ...mapGetters([
      'name'
    ]),
  },
  created: function() {
    this.GetInfo();
  },
  methods: {
    Logout(e) {
      e.preventDefault();
      this.$store.dispatch('LogOut').then(() => {
        this.$router.push({ path: '/login' });
      }).catch(err => {
        this.$message.error(err);
      });
    },
    click() {
      // do nothing
    },
    sidebarToggle(e) {
      e.preventDefault()
      document.body.classList.toggle('sidebar-hidden')
    },
    sidebarMinimize(e) {
      e.preventDefault()

      document.body.classList.toggle('sidebar-minimized')
    },
    mobileSidebarToggle(e) {
      e.preventDefault()

      document.body.classList.toggle('sidebar-mobile-show')
    },
    asideToggle(e) {
      e.preventDefault()
      document.body.classList.toggle('aside-menu-hidden')
    },
    ...mapActions([
      'GetInfo'
    ])
  }
}
</script>

<style type="text/css">
.dropdown-itemp {
  text-align: left;
  font-size: 15px;
  padding: 10px;
}
</style>
