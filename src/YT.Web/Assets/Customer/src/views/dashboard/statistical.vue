<template>
    <div class="statistical">
        <head-top-tag></head-top-tag>
        <div  class="settle"style="min-height:calc(100% - 92px)">
            <Row class="settleTop">
                <Col span="24">结算明细</Col>
            </Row>
            <div class="settleMain">
                <table class="table">
                    <thead>
                        <tr>
                            <th class="g-font14">交易时间</th>
                            <th>账户金额</th>
                            <th>本次消费</th>
                            <th>账户余额</th>
                            <th style="width:160px; padding-right:14px">
                                <Select @on-change="Init" v-model="record" placeholder="最近三个月交易记录" style="color:#c2c2c2" >
                                    <Option v-for="item in options" :value="item.value" :key="item.value">{{ item.label }}</Option>
                                </Select>
                            </th>
                        </tr>
                    </thead>
                    <tbody>
                        <tr v-for="item in records" :key="item.id">
                            <td class="g-font14">{{ item.creationTime |formatDate}}</td>
                            <td>￥{{ item.balance }}</td>
                            <td>￥{{ item.cost }}</td>
                            <td>￥{{ item.currentBalance }}</td>
                            <td></td>
                        </tr>
                    </tbody>
                </table>
            </div>
            <Row class='footbtn'>
                <Col span='24'>
                <Button type='primary' @click="backHome">完成</Button>
                </Col>
            </Row>
        </div>
        <footer-tag></footer-tag>
    </div>
</template>

<script>
import HeadTopTag from "components/HeadTop";
import FooterTag from "components/Footer";
import { costs } from "api/product";
import { parseTime } from "utils/index";
export default {
  data() {
    return {
      options: [
        {
          value: 3,
          label: "最近三个月交易记录"
        },
        {
          value: 6,
          label: "最近六个月交易记录"
        }
      ],
      record: 3,
      records: []
    };
  },
  // 过滤器
  filters: {
    formatDate(time) {
      return parseTime(time);
    }
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
      const id = this.$route.query.id;
      const customerId = localStorage.getItem("Credit-Id");
      if (!customerId) {
        this.$router.push({ path: "/login" });
        return;
      }
      costs({ num: this.record, customerId: customerId })
        .then(r => {
          if (r.data.success) {
            this.records = r.data.result;
          }
        })
        .catch(e => {
          this.$Message.error(e.response.data.error.message);
        });
    },
    backHome() {
      this.$router.push({ path: "/dashboard" });
    }
  }
};
</script>
<style rel="stylesheet/scss" lang="scss">
.statistical {
  height: 100%;
}
.settle {
  padding: 96px 188px;
  .settleTop {
    width: 100%;
    font-size: 18px;
    color: #4c5b70;
    margin-bottom: 51px;
  }
  .settleMain {
    width: 100%;
    text-align: center;
    .table {
      width: 100%;
      margin: 0 auto;
      border-collapse: collapse;
      margin-bottom: 54px;
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
        height: 70px;
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
          width: 270px;
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

