<template>
    <div>
        <table style="width:100%;" border="2">
            <thead>
                    <tr> <td>序号</td>
                    <td>公司名称</td>
                    <td>联系人</td>
                    <td>定价</td></tr>
                   
            </thead>
            <tbody>
                <tr v-for="(d,i) in list" :key="i">
                    <td>{{++i}}</td>
                    <td>{{d.customerName}}</td>
                    <td>{{d.contact}}</td>
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
