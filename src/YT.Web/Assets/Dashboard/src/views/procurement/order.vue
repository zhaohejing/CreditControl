<template>
    <div class="animated fadeIn">
        <Row>
            <milk-table ref="list" :layout="[20,2,2]"  :columns="cols" :search-api="searchApi" :params="params">
                <template slot="search">
               <Form ref="params" :model="params" inline :label-width="60">
                        <FormItem label="联系人">
                            <Input v-model="params.contact" placeholder="请输入联系人"></Input>
                        </FormItem>
                        <FormItem label="商品名">
                            <Input v-model="params.product" placeholder="请输入商品名"></Input>
                        </FormItem>
                          <FormItem label="代理商">
                            <Input v-model="params.account" placeholder="请输入代理商"></Input>
                        </FormItem>
                   <FormItem label="状态">
                    <Select v-model="params.state" style="width:160px">
                       <Option value="0">请选择</Option>
                       <Option value="1">已完成</Option>
                       <Option value="2">已取消</Option>
                       <Option value="3">未完成</Option>
                    </Select>
                   </FormItem>
                <FormItem label="是否表单">
                    <Select v-model="params.requireForm" style="width:160px">
                       <Option value="">请选择</Option>
                       <Option value="true">是</Option>
                       <Option value="false">否</Option>
                    </Select>
                </FormItem>
                        <FormItem label="开始时间">
                            <DatePicker type="date" :editable="false" v-model="params.start" placeholder="开始时间" style="width: 140px"></DatePicker>
                        </FormItem>
                        <FormItem label="截至时间">
                            <DatePicker type="date" :editable="false" v-model="params.end" placeholder="截至时间" style="width: 140px"></DatePicker>
                 </FormItem>
               </Form>
                </template>
             <template slot='actions'>
                    <Button @click.native='exportData' type='primary'>导出</Button>
             </template>   
            </milk-table>
        </Row>
    </div>
</template>

<script>
import { orders, order, payBack, exportOrder } from "api/products";
export default {
  name: "account",
  data() {
    return {
      cols: [
        {
          title: "订单号",
          key: "orderNum"
        },
        {
          title: "商品名称",
          key: "productName"
        },
        {
          title: "消费价格",
          key: "totalPrice"
        },
        {
          title: "联系人",
          key: "contact"
        },
        {
          title: "代理商名称",
          key: "companyName"
        },
        {
          title: "创建时间",
          key: "creationTime",
          render: (h, params) => {
            return this.$fmtTime(params.row.creationTime);
          }
        },
        {
          title: "状态",
          key: "isActive",
          render: (h, params) => {
            return params.row.state != null
              ? params.row.state != null && params.row.state ? "已完成" : "已取消"
              : "未完成";
          }
        },
        {
          title: "操作",
          key: "action",
          width: "130px",
          align: "center",
          render: (h, params) => {
            const childs = [];
            childs.push(
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
                      this.detail(params.row);
                    }
                  }
                },
                "查看"
              )
            );
            if (params.row.state) {
              childs.push(
                h(
                  "Button",
                  {
                    props: {
                      type: "primary",
                      size: "small"
                    },
                    on: {
                      click: () => {
                        this.payback(params.row);
                      }
                    }
                  },
                  "退款"
                )
              );
            }
            return h("div", childs);
          }
        }
      ],
      searchApi: orders,
      params: {
        contact: "",
        product: "",
        account: "",
        requireForm: null,
        state: 0,
        start: null,
        end: null
      },
      modal: {
        isShow: false,
        title: "订单详情",
        current: null
      }
    };
  },
  created() {
    var self = this;
    self.$root.eventHub.$on("order", () => {
      self.cancel();
    });
  },
  destroyed() {
    this.$root.eventHub.$off("order");
  },
  methods: {
    // 完成
    payback(model) {
      var table = this.$refs.list;
      this.$Modal.confirm({
        title: "操作提示",
        content: "确定要退款么?",
        onOk: () => {
          const parms = { id: model.id };
          payBack(parms)
            .then(c => {
              if (c.data.success) {
                table.initData();
              }
            })
            .catch(e => {
              this.$Message.error(e.response.data.error.message);
            });
        }
      });
    },
    exportData() {
      exportOrder(this.params)
        .then(r => {
          if (r.data.success) {
            let file = r.data.result;
            let url =
              "http://47.93.2.82:9999/api/File/Download?fileType=" +
              file.fileType +
              "&fileToken=" +
              file.fileToken +
              "&fileName=" +
              file.fileName;
            window.open(url);
          }
        })
        .catch(e => {
          this.$Message.error(e.response.data.error.message);
        });
    },
    detail(row) {
      this.$router.push({ path: "/order", query: { id: row.id } });
    },
    save() {
      this.$refs.account.commit();
    },
    cancel() {
      this.modal.isShow = false;
      this.modal.current = null;
      this.$refs.list.initData();
    }
  },
  mounted() {}
};
</script>


<style type="text/css" scoped>

</style>