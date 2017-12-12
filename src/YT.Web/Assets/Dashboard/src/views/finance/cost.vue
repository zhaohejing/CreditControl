<template>
    <div class="animated fadeIn">
        <Row>
            <milk-table ref="list" :layout="[22,2]" :columns="cols" :search-api="searchApi" :params="params">
                <template slot="search">
                    <Form ref="params" :model="params" inline :label-width="60">
                        <FormItem label="公司名">
                            <Input v-model="params.name" placeholder="公司名"></Input>
                        </FormItem>
                        <FormItem label="开始时间">
                            <DatePicker type="date" :editable="false" v-model="params.start" placeholder="开始时间" style="width: 140px"></DatePicker>
                        </FormItem>
                        <FormItem label="截至时间">
                            <DatePicker type="date" :editable="false" v-model="params.end" placeholder="截至时间" style="width: 140px"></DatePicker>
                        </FormItem>
                    </Form>
                </template>

            </milk-table>
        </Row>
    </div>
</template>

<script>
import { costs } from "api/record";
export default {
  name: "costs",
  data() {
    return {
      cols: [
        {
          title: "公司名",
          key: "customerName"
        },

        {
          title: "余额",
          key: "balance"
        },
         {
          title: "消费",
          key: "cost"
        },
        
        {
          title: "当前剩余",
          key: "currentBalance"
        },
        {
          title: "创建时间",
          key: "creationTime",
          render: (h, params) => {
            return this.$fmtTime(params.row.creationTime);
          }
        }
      ],
      searchApi: costs,
      params: { customerName: "",  start: null, end: null },
      chargeModal: {
        isShow: false,
        title: "客户充值",
        current: { money: 0, id: null }
      }
    };
  },
  created() {},
  destroyed() {},
  methods: {
    charge(row) {
      this.chargeModal.isShow = true;
      this.chargeModal.title = this.chargeModal.title + ": " + row.customerName;
      this.chargeModal.current.id = row.id;
    },
    commitCharge() {
      apply(this.chargeModal.current)
        .then(r => {
          if (r.data.success) {
            this.$refs.list.initData();
          }
        })
        .catch(e => {
          this.$Message.error(e.message);
        });
    },
    delete(row) {
      const table = this.$refs.list;
      this.$Modal.confirm({
        title: "删除提示",
        content: "确定要删除当前记录么?",
        onOk: () => {
          const parms = { id: row.id };
          clear(parms).then(c => {
            if (c.data.success) {
              table.initData();
            }
          });
        }
      });
    },
    cancel() {
      this.chargeModal.isShow = false;
      this.chargeModal.title = "客户充值";
      this.chargeModal.current.money = 0;
      this.$refs.list.initData();
    }
  }
};
</script>


<style type="text/css" scoped>

</style>