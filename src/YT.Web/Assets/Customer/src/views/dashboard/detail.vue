<template>
    <div class="detail">
        <Row>
            <Col span="24">
            <Icon type="chevron-left"></Icon>
            <a @click="returnUp">返回上一页</a>
            </Col>
        </Row>
        <div class="detialMain">
            <Row class="MainBox">
                <div class="detailBox">
                    <div class="detailImg g-center">
                        <img style="width:400px;height:200px;" :src="product.profileUrl" />
                        <div class="tag">{{ product.levelTwoName }}</div>
                    </div>
                    <div class="detailCon">
                        <h3>{{ product.productName }}</h3>
                        <p v-html="product.content"></p>
                    </div>

                </div>
                <Row class="detialFootTag">
                    <div class="g-footPrice">
                        <span class="singlePrice">单价：{{product.price}}元</span>
                        <div v-if="!product.requireForm" class="unitPrice">
                            <Button type="ghost" class="g-add" @click="minus"  :disabled="product.count<=1">-
                            </Button><Input class="input" v-model="product.count" style="width:60px">
                            </Input><Button type="ghost"  class="g-add" @click="plus">+</Button>
                        </div>
                        <Button class="order" @click="order" type="primary">立即预定</Button>
                    </div>
                </Row>
            </Row>
        </div>
    </div>
</template>
<script>
import { detail, createOrder } from "api/product";
export default {
  data() {
    return {
      product: { profileUrl: "" },
      from: "/dashboard",
      params: {
        customerId: localStorage.getItem("Credit-Id"),
        cateId: null
      }
    };
  },
  created() {
    this.init();
  },
  activated() {
    this.from = this.$route.query.from;
    this.init();
  },
  methods: {
    init() {
      this.params.cateId = this.$route.query.id;
      detail(this.params)
        .then(r => {
          if (r.data.success) {
            this.product = r.data.result;
          }
        })
        .catch(e => {
          this.$Message.error(e.response.data.error.message);
        });
    },
    order() {
      const param = {
        customerId: localStorage.getItem("Credit-Id"),
        count: this.product.count,
        productId: this.product.id
      };
      createOrder(param)
        .then(r => {
          if (r.data.success) {
            this.$Message.success("订购成功");
            const orderId = r.data.result.id;
            if (this.product.item.requireForm) {
              this.$router.push({ path: "/info", query: { orderId: orderId } });
            } else {
              this.$router.push({ path: "/order" });
            }
          }
        })
        .catch(e => {
          this.$Message.error(e.response.data.error.message);
        });
    },
    minus() {
      this.product.count--;
    },
    plus() {
      this.product.count++;
    },
    // 返回上一页
    returnUp() {
      this.$router.push({ path: this.from, query: { id: null } });
    }
  }
};
</script>

<style rel="stylesheet/scss" lang="scss">
.g-center {
  text-align: center;
}

.detail {
  padding: 40px 106px;
  height: 100%;
  a {
    color: #383d41;
  }
  .detialMain {
    width: 100%;
    padding: 22px 48px 48px 48px;
  }
  .MainBox {
    box-shadow: 0px 0px 7px 4px rgba(1, 34, 63, 0.1);
  }
  .detailBox {
    padding: 30px 30px 66px 30px;
    .detailImg {
      border-bottom: 1px solid #ebebeb;
      .tag {
        width: 120px;
        height: 30px;
        background: #ccddf4;
        font-size: 14px;
        color: #679feb;
        text-align: center;
        line-height: 30px;
        margin: 20px auto;
      }
    }
    .detailCon {
      padding: 24px 174px;
      h3 {
        font-weight: normal;
        font-size: 15px;
        color: #4d4d4d;
      }
      p {
        line-height: 26px;
        font-size: 14px;
        color: #999;
        padding-top: 12px;
      }
    }
  }
  .detialFootTag {
    width: 100%;
    height: 60px;
    background: #f7f7f7;
    line-height: 60px;
    .g-footPrice {
      width: 100%;
      text-align: right;
      padding-right: 58px;
    }
    .singlePrice {
      color: #679feb;
      font-size: 15px;
    }
    .unitPrice {
      width: 120px;
      height: 26px;
      line-height: 26px;
      display: inline;
      margin: 0 44px;
    }
    .g-add {
      width: 28px;
      height: 26px;
      line-height: 26px;
      text-align: center;
      padding: 0;
      color: #ccc;
      border: 1px solid #bbb;
      border-radius: 0;
    }
    .order {
      width: 102px;
      height: 26px;
      background: #4f5873;
      text-align: center;
      line-height: 26px;
      padding: 0;
      border: 1px solid #4f5873;
      border-radius: 0;
    }
    .input input {
      height: 26px;
      text-align: center;
      line-height: 26px;
      border-radius: 0;
      border-left: none;
      border-right: none;
      background: #f7f7f7;
      border: 1px solid #bbb;
    }
  }
}
</style>
