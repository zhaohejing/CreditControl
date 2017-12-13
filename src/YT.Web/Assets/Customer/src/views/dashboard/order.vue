<template>
  <div class="order">
    <head-top-tag></head-top-tag>
     <Row class='infoTitle'>
        <Col span='18' ><p class='title'>订单信息</p></Col>
        <Col span='6' ><div @click.native="exportData" class='export'>导出订单</div></Col>
      </Row>
       <hr style="height:5px;border:none;border-top:5px groove"/>
    <div v-if="orders.length>0" class="orderMain">
      <div v-for="item in orders" :key="item.id">
        <Row class="orderTop">
          <Col span="12">订单号：{{ item.orderNum }}</Col>
          <Col span="8" offset="4" class="text-right">{{ item.creationTime | formatDate }}</Col>
        </Row>
        <Row class="orderRow">
          <Col span="2">
          <img :src="item.profile">
          </Col>
          <Col span="21" class="g-text">
          <a @click="linkToDetail(item)">{{ item.productName }}({{item.cate}}) </a>
          </Col>
        </Row>
        <Row class="orderCon">
          <span class="customer">客户名称:{{ item.customerName }}</span>
          <span>单价：{{ item.price }}元</span>
          <span>数量：{{ item.count }}</span>
          <span>合计：￥{{ item.totalPrice }}</span>
          <span class="g-color" @click="modifyForm(item)">
            <font v-if="item.requireForm &&item.state==null " v-bind:class="[{ hiddenAcitve: item.state }, errorClass]">
              修改客户资料
            </font>
          </span>
          <span style="color:#c47373">
            <!-- <font @click="cancelOrder(item)" v-if="item.state==null" v-bind:class="[{ colorAcitve: item.state },errorClass]">
              取消订单
            </font> -->
            <font v-if="item.state" v-bind:class="[{ colorAcitve: item.state },errorClass]">
              已完成
            </font>
            <font v-if="item.state!=null&&!item.state" v-bind:class="[{ colorAcitve: item.state },errorClass]">
              已取消
            </font>
          </span>

        </Row>
      </div>

      <Row>
        <Col :span="4">
        <p @click="prev"> 上一页</p>
        </Col>
        <Col :offset="23" :span="4">
        <p @click="next"> 下一页</p>
        </Col>
      </Row>
    </div>
    <div class="orderMain" v-else>
      <div class="headBox">
        <div class="g-box">
          <img :src="imgUrl">
          <Row class="g-left g-pt40">
            <col span="24">
            <font>暂无订单</font>
            </col>
          </Row>
          <Row class="g-left">
            <col span="24"> 如有疑问请联系010-56261409
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
import { orders, exportOrders } from "api/product";
import { parseTime } from "utils/index";
export default {
  data() {
    return {
      errorClass: "",
      imgUrl: "../../../static/img/wait2.png",
      orders: [{}],
      total: 0,
      index: 1,
      params: {
        customerId: localStorage.getItem("Credit-Id"),
        sorting: "",
        maxResultCount: 10,
        skipCount: 0
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
  // 过滤器
  filters: {
    formatDate(time) {
      return parseTime(time);
    }
  },
  methods: {
    prev() {
      this.index--;
      if (this.index <= 0) {
        this.index = 1;
      }
      this.params.skipCount = (this.index - 1) * this.params.maxResultCount;
      this.Init();
    },
    next() {
      this.index++;
      this.params.skipCount = (this.index - 1) * this.params.maxResultCount;

      this.Init();
    },
    Init() {
      const id = localStorage.getItem("Credit-Id");
      if (!id) {
        this.$router.push({
          path: "/login"
        });
        return;
      }
      this.params.customerId = id;
      orders(this.params).then(r => {
        if (r.data.success) {
          this.orders = r.data.result.items;
          this.total = r.data.result.totalCount;
        }
      });
    },
    linkToDetail(item) {
      this.$router.push({
        path: "/detail",
        query: {
          id: item.productId,
          from: "/order"
        }
      });
    },
    // 修改客户资料
    modifyForm(item) {
      this.$router.push({
        path: "/info",
        query: {
          orderId: item.id
        }
      });
    },
    // 取消订单
    cancelOrder(item) {
      this.$router.push({
        path: "/cancelorder",
        query: {
          id: item.id
        }
      });
    },
    exportData() {
      exportOrders({ customerId: this.customerId }).then(r => {
        if (r.data.success) {
          if (r.success) {
            this.$down(
              r.data.result.fileType,
              r.data.result.fileToken,
              r.data.result.fileName
            );
          }
        }
      });
    }
  }
};
</script>
<style rel="stylesheet/scss" lang="scss" scoped>
.infoTitle {
  padding-top: 10px;
  padding-bottom: 10px;
  font-size: 20px;
  color: #373d41;
  .title {
    margin-left: 270px;
    text-align: center;
  }
  .export {
    width: 100px;
    height: 30px;
    line-height: 30px;
    background-color: #2d8cf0;
    font-size: 14px;
    text-align: center;
    margin-left: 50px;
  }
}

.order {
  height: 100%;
  .orderMain {
    min-height: calc(100% - 92px);
    padding: 65px 145px;
    .orderTop {
      width: 100%;
      height: 40px;
      background: #f7f7f7;
      line-height: 40px;
      .ivu-col {
        padding: 0 20px;
        font-size: 14px;
        color: #373d41;
      }
      .text-right {
        text-align: right;
        padding-left: 30px;
      }
    }
    .orderRow {
      padding: 10px 20px 0;
      img {
        width: 120px;
        height: 60px;
      }
      .g-text {
        padding-left: 50px;
        font-size: 14px;
        padding-top: 14px;
        a {
          color: #679feb;
        }
      }
    }
    .orderCon {
      text-align: right;
      padding-bottom: 24px;
      border-bottom: 1px solid #c3c2c2;
      .customer {
        display: inline-block;
        padding-right: 435px;
        font-size: 14px;
        min-width: 100px;
        overflow: hidden;
      }
      span {
        display: inline-block;
        padding-right: 44px;
        font-size: 14px;
        min-width: 100px;
        overflow: hidden;
      }
      span:last-child {
        cursor: pointer;
      }
      span:nth-child(4) {
        cursor: pointer;
      }
      .g-color {
        color: #c47373;
      }
      .hiddenAcitve {
        display: none;
      }
      .colorAcitve {
        color: #808080 !important;
      }
    }
    .headBox {
      width: 996px;
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
        .g-left {
          text-align: left;
        }
        .g-pt40 {
          padding-top: 40px;
          padding-bottom: 26px;
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
