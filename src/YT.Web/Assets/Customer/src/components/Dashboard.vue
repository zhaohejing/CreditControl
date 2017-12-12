<template>
    <div class="Dashboard">
        <head-top-tag></head-top-tag>
        <div class="dashboardMain">
            <div class="g-leftMenu">
                <row>
                    <i-col span="24">
                        <i-menu :theme="theme2">
                            <submenu v-if="item.children" name="1" v-for="item in menu" :key="item.id">
                                <template slot="title">
                                    <img :src="item.iconUrl"> {{ item.displayName}}
                                </template>
                                <menu-item name="1-1" class="twoChildMenu" v-for="item in item.children" :key="item.id">
                                    <submenu name="2">
                                        <template slot="title">
                                            <Icon type="arrow-down-b"></Icon>
                                            {{ item.displayName }}
                                        </template>
                                        <menu-item name="1-1-1" v-for="item in item.children" :key="item.id">
                                            <a @click="LinkToMenu(item)">{{ item.displayName }}</a>
                                        </menu-item>
                                    </submenu>
                                </menu-item>
                            </submenu>
                            <menu-item class="ivu-menu-submenu-title" name="1" v-else :key="item.id">
                                <img :src="item.iconUrl">
                                <a @click="LinkToMenu(item)">{{ item.displayName }}</a>
                            </menu-item>

                        </i-menu>
                    </i-col>
                </row>
            </div>
            <div class="g-rightMain">
                <keep-alive>
                    <router-view></router-view>
                </keep-alive>
            </div>
        </div>
        <footer-tag></footer-tag>
    </div>
</template>
<script>
import HeadTopTag from "components/HeadTop";
import FooterTag from "components/Footer";
import { menus } from "api/login";
export default {
  data() {
    return {
      theme2: "dark",
      menu: [],
      childMenu: [],
      lastChildMenu: []
    };
  },
  components: {
    FooterTag,
    HeadTopTag
  },
  created() {
    this.Init();
  },
  methods: {
    LinkToMenu(item) {
      this.$router.push({ path: item.url, query: { id: item.id } });
    },
    Init() {
      menus({ id: localStorage.getItem("Credit-Id") }).then(r => {
        if (r && r.data && r.data.success) {
          const childs = r.data.result;
          this.menu = [
            {
              id: 1,
              displayName: "栏目与服务采购",
              iconUrl: "../../static/img/icon01.png",
              children: childs
            },
            {
              id: 2,
              displayName: "用户设置",
              iconUrl: "../../static/img/icon02.png",
              url: "/setting"
            },
            {
              id: 3,
              displayName: "客户服务",
              iconUrl: "../../static/img/icon03.png",
              url: "/adsence"
            }
          ];
        }
      });
    }
  }
};
</script>
<style rel="stylesheet/scss" lang="scss">
.Dashboard {
  height: 100%;
  .dashboardMain {
    width: 100%;
    height: calc(100% - 92px);
    .g-leftMenu {
      float: left;
      width: 200px;
      background: #333743;
      height: 100%;
      overflow: auto;
      a {
        color: #fff;
        cursor: pointer;
      }
      img {
        vertical-align: middle;
        padding-right: 12px;
      }
      .on {
        color: #679feb;
      } // 重写iview导航菜单样式
      .ivu-menu-vertical .ivu-menu-submenu-title {
        height: 40px;
        line-height: 40px;
        padding: 0 0 0 30px;
      }
      .ivu-menu-submenu .ivu-menu-item-active,
      .ivu-menu-submenu .ivu-menu-item-active:hover {
        background: none !important;
      }
      .ivu-menu-item-active:not(.ivu-menu-submenu),
      .ivu-menu-submenu-title-active:not(.ivu-menu-submenu) {
        background: none;
        border: none;
      }
      .ivu-menu-dark {
        background: #4f5873;
        color: #fff;
      }
      .ivu-menu-dark.ivu-menu-vertical .ivu-menu-submenu-title {
        color: #b3b3b3;
      }
      .ivu-menu-opened > .ivu-menu-submenu-title {
        color: #fff !important;
      }
      .ivu-menu {
        width: 100% !important;
      }
      .ivu-icon-ios-arrow-down {
        display: none;
      }
      .twoChildMenu .ivu-menu-submenu-title {
        background: none;
      }
      .twoChildMenu {
        padding: 0;
        .ivu-menu-submenu-title {
          text-align: left;
          padding-left: 50px;
        }
        .ivu-menu-item {
          padding-left: 68px;
        }
      }
    }
    .g-rightMain {
      width: calc(100% - 200px);
      height: 100%;
      float: left;
      overflow: auto;
    }
  }
}
</style>