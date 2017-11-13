<template>
    <div>
        <table style="width:100%;" border="2">
            <thead>
                    <tr> <td>序号</td>
                    <td>商品名称</td>
                    <td>所属分类</td>
                    <td>定价</td></tr>
                   
            </thead>
            <tbody>
                <tr v-for="(d,i) in list" :key="i">
                    <td>{{++i}}</td>
                    <td>{{d.productName}}</td>
                    <td>{{d.levelName}}</td>
                    <td> <InputNumber  :precision="0" v-model="d.price"></InputNumber></td>
                </tr>
            </tbody>
        </table>
        <Row>
            <Col :offset="8" :span="4">
            <Button @click="back">返回</Button>
            </Col>
                <Col  :span="4">
    <Button @click="commit" type="primary">提交</Button>
            </Col>
        </Row>
    </div>
</template>
<script>
import { prices } from "api/customer";
import { pricing } from "api/products";
export default {
  nmae: "customerpricing",
  components: {},
  data() {
    return {
      list: [],
      params: {
        id: this.$route.query.customerId
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
      this.$router.push({ path: "customers", query: {} });
    },
    commit() {
      pricing({ customerPrices: this.list })
        .then(response => {
          if (response.data.success) {
            this.$Message.success("操作成功");
            this.$router.push({ path: "customers", query: {} });
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
