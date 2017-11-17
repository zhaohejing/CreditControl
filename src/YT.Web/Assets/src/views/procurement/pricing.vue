<template>
<div class="statistical">
        <div  class="settle">
            <Row class="settleTop">
                <Col span="24">客户定价</Col>
            </Row>
            <div class="settleMain">
                <table class="table">
                    <thead>
                        <tr>
                            <th class="g-font14">序号</th>
                            <th>公司名称</th>
                            <th>联系人</th>
                            <th>定价</th>
                        </tr>
                    </thead>
                    <tbody>
                          <tr v-for="(d,i) in list" :key="i">
                    <td class="g-font14">{{++i}}</td>
                    <td>{{d.customerName}}</td>
                    <td>{{d.contact}}</td>
                    <td> <InputNumber :min="1" :precision="2" v-model="d.price"></InputNumber></td>
                </tr>
                    </tbody>
                </table>
            </div>
            <Row class='footbtn'>
                  <Col offset="6" span='4'>
                <Button  type='primary' @click="back">返回</Button>
                </Col>
                <Col offset="2" span='4'>
                <Button type='primary' @click="commit">完成</Button>
                </Col>
            </Row>
        </div>
    </div>
   
</template>
<script>
import { prices, pricing } from "api/products";
export default {
  nmae: "pricing",
  components: {},
  data() {
    return {
      list: [],
      params: {
        id: this.$route.query.productId
      }
    };
  },
  created() {
    this.init();
  },
  methods: {
    init() {
      prices(this.params).then(c => {
        if (c.data.success) {
          this.list = c.data.result;
        }
      });
    },
    back() {
      this.$router.push({ path: "products", query: {} });
    },
    commit() {
      pricing({ customerPrices: this.list })
        .then(response => {
          if (response.data.success) {
            this.$Message.success("操作成功");
            this.$router.push({ path: "products", query: {} });
          } else {
            this.$Message.success("操作失败");
          }
        })
        .catch(erroe => {
          this.$Message.error(erroe.error);
        });
    }
  }
};
</script>
<style lang="scss" scoped>
.statistical {
  height: 100%;
}
.settle {
  .settleTop {
    width: 100%;
    font-size: 18px;
    color: #4c5b70;
    margin-bottom: 1px;
  }
  .settleMain {
    width: 100%;
    text-align: center;
    .table {
      width: 100%;
      margin: 0 auto;
      border-collapse: collapse;
      margin-bottom: 4px;
      thead {
        background: #f5f5f5;
        tr {
          height: 48px;
          line-height: 48px;
          border-bottom: none;
        }
        th {
          color: #8d919a;
          font-size: 14px;
          font-weight: normal;
          width: 100px;
          padding-left: 30px;
          text-align: left;
          select {
            width: 163px;
            font-size: 14px;
          }
          .ivu-select-selection {
            border-radius: 0;
          }
        }
      }
      tr {
        border-bottom: 1px solid #ebebeb;
        td {
          font-size: 18px;
          color: #474e5c;
          width: 187px;
          padding-left: 30px;
          text-align: left;
        }
        .g-font14 {
          font-size: 14px;
          width: 100px;
        }
      }
    }
  }
  .footbtn {
    margin-top: 120px;
    text-align: center;
    button {
      width: 200px;
      height: 40px;
      text-align: center;
      line-height: 40px;
      background: #679fec;
      border-radius: 0;
      border: 1px solid #679fec;
      font-size: 14px;
      padding: 0;
    }
  }
}
</style>
