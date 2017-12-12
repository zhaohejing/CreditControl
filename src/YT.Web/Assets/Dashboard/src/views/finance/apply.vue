<template>
    <div class="animated fadeIn">
        <Row>
            <milk-table ref="list" :layout="[22,2]" :columns="cols" :search-api="searchApi" :params="params">
                <template slot="search">
                    <Form ref="params" :model="params" inline :label-width="60">
                        <FormItem label="公司名">
                            <Input v-model="params.name" placeholder="公司名"></Input>
                        </FormItem>
                        <FormItem label="金额">
                            <Input v-model="params.money" placeholder="金额"></Input>
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
        <!-- 充值窗口 -->
        <Modal :transfer='false' v-model='chargeModal.isShow' :title='chargeModal.title' :mask-closable='false' @on-ok='commitCharge' @on-cancel='cancel'>
            <Form ref="charge" :model="chargeModal.current" inline :label-width="120">
                <FormItem label="充值金额" prop="name">
                    <InputNumber :min="1" v-model="chargeModal.current.money" placeholder="充值金额"></InputNumber>
                </FormItem>
            </Form>
        </Modal>
    </div>
</template>

<script>
import { applys, apply, clear } from "api/record";
export default {
  name: "account",
  data() {
    return {
      cols: [
        {
          title: "公司名",
          key: "customerName"
        },

        {
          title: "充值金额",
          key: "chargeMoney"
        },
         {
          title: "实到金额",
          key: "actrueMoney"
        },
        
        {
          title: "操作人",
          key: "actionName"
        },
        {
          title: "状态",
          key: "state",
          render: (h, params) => {
            return params.row.state == null
              ? "未处理"
              : params.row.state ? "已处理" : "已取消";
          }
        },
        {
          title: "创建时间",
          key: "creationTime",
          render: (h, params) => {
            return this.$fmtTime(params.row.creationTime);
          }
        },
        {
          title: "操作",
          key: "action",
          align: "center",
          width: "250px",
          render: (h, params) => {
            let children = [];
            if (params.row.state == null) {
              children.push(
                h(
                  "Button",
                  {
                    props: {
                      type: "primary",
                      size: "small"
                    },
                    style: {
                      marginRight: "5px"
                    },
                    on: {
                      click: () => {
                        this.charge(params.row);
                      }
                    }
                  },
                  "充值"
                )
              ); //组件1
              children.push(
                h(
                  "Button",
                  {
                    props: {
                      type: "error",
                      size: "small"
                    },
                    on: {
                      click: () => {
                        this.delete(params.row);
                      }
                    }
                  },
                  "删除"
                )
              ); //组件2
            }

            return h("div", children);
          }
        }
      ],
      searchApi: applys,
      params: { name: "", money: "", start: null, end: null },
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