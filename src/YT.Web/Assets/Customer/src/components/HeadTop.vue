<template>
  <div class="headtop">
    <Row class="g-regHeader">
      <div @click="home" class="logo">
        <Col>{{ logoTag }}</Col>
      </div>
      <div class="header_right">
        <Dropdown placement="bottom-start" @on-click="goto">
            <span class="g-center g-leftBorder s1">账户&nbsp;&nbsp;<img :src="imgIcon"></span>
            <DropdownMenu slot="list">
              <DropdownItem name="charge">充值</DropdownItem>
              <DropdownItem name="order">订单</DropdownItem>
              <DropdownItem name="statistical">结算明细</DropdownItem>
            </DropdownMenu>
        </Dropdown>
        <span class="g-center g-leftBorder s2">{{ name }}</span>
        <span class="g-center g-leftBorder s3">
          <a @click="exit">退出</a>
        </span>
      </div>
    </Row>
  </div>
</template>

<script>
export default {
  data() {
    return {
      logoTag: "代理商管理平台--首页",
      imgIcon: "../../static/img/account.png"
    };
  },
  computed: {
    name: function() {
      return localStorage.getItem("Credit-Name");
    }
  },
  methods: {
    home() {
      this.$router.push({ path: "/infos", query: { id: null } });
    },
    exit() {
      localStorage.clear();
      this.$Message.success("退出成功，请重新登陆!");
      this.$router.push({ path: "/login" });
    },
    goto(name) {
      if (name === "charge") {
        this.$router.push({ path: "/charge" });
      } else if (name === "order") {
        this.$router.push({ path: "/order" });
      } else {
        this.$router.push({ path: "/statistical" });
      }
    }
  }
};
</script>

<style rel="stylesheet/scss" lang="scss">
.g-center {
  text-align: center;
}

.headtop {
  height: 50px;
  line-height: 50px;
  background: #383d41;
  .g-regHeader {
    height: 50px;
    line-height: 50px;
    background: #383d41;
    font-size: 12px;
    color: #fff;
    .logo {
      float: left;
      width: 400px;
      padding-left: 32px;
      &:hover {
        cursor: pointer;
      }
    }
    .header_right {
      float: right;
      width: 500px;
      height: 50px;
      text-align: right;
    }
    .s1 {
      width: 74px;
    }
    .s1:hover {
      background: #2a2f32;
    }
    .ivu-select-dropdown {
      width: 150px;
      padding: 0;
    }
    .ivu-dropdown-item {
      background: #fff;
      height: 40px;
      line-height: 40px;
      padding: 0;
      color: #444;
      text-align: left;
      padding-left: 15px;
    }
    .ivu-dropdown-item:hover {
      background: #a6d3ff;
    }
    .s2 {
      padding: 0 18px;
    }
    .s3 {
      width: 75px;
    }
    .g-leftBorder {
      border-left: 1px solid #2a2f32;
      margin-right: -3px;
    }
    span {
      display: inline-block;
    }
    .g-center {
      a {
        color: #fff;
      }
      &:hover {
        cursor: pointer;
      }
    }
  }
}
</style>
