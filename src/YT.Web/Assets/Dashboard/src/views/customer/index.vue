<template>
    <div class='animated fadeIn'>
        <Row>
            <milk-table ref='list' :layout='[18,3,3]' :columns='cols' :search-api='searchApi' :params='params'>
                <template slot='search'>
                    <Form ref='params' :model='params' inline :label-width='60'>
                        <FormItem label='公司名称'>
                            <Input v-model='params.companyName' placeholder='请输入公司名称'></Input>
                        </FormItem>
                        <FormItem label='联系人'>
                            <Input v-model='params.contact' placeholder='联系人'></Input>
                        </FormItem>
                        <FormItem label='所在城市'>
                            <Input v-model='params.city' placeholder='所在城市'></Input>
                        </FormItem>
                    </Form>
                </template>
            </milk-table>
        </Row>
        <!-- 详情 -->
        <Modal :transfer='false' v-model='detailModal.isShow' :title='detailModal.title' :mask-closable='false' @on-ok='save' @on-cancel='cancel'>
            <Row>
                <Col :span="4">
                <p>企业名称:</p>
                </Col>
                <Col :span="8">
                <p>{{detailModal.current.companyName }}</p>
                </Col>
                <Col :span="4">
                <p>省份:</p>
                </Col>
                <Col :span="8">
                <p>{{detailModal.current.provence }}</p>
                </Col>
            </Row>
            <Row>
                <Col :span="4">
                <p>城市:</p>
                </Col>
                <Col :span="8">
                <p>{{detailModal.current.city }}</p>
                </Col>
                <Col :span="4">
                <p>详细地址:</p>
                </Col>
                <Col :span="8">
                <p>{{detailModal.current.address }}</p>
                </Col>
            </Row>
            <Row>
                <Col :span="4">
                <p>联系人:</p>
                </Col>
                <Col :span="8">
                <p>{{detailModal.current.contact }}</p>
                </Col>
                <Col :span="4">
                <p>手机:</p>
                </Col>
                <Col :span="8">
                <p>{{detailModal.current.mobile }}</p>
                </Col>
            </Row>
            <Row>
                <Col :span="4">
                <p>邮箱:</p>
                </Col>
                <Col :span="8">
                <p>{{detailModal.current.email }}</p>
                </Col>
                <Col :span="4">
                <p>账户:</p>
                </Col>
                <Col :span="8">
                <p>{{detailModal.current.account }}</p>
                </Col>
            </Row>
            <Row>
                <Col :span="4">
                <p>余额:</p>
                </Col>
                <Col :span="8">
                <p>{{detailModal.current.balance }}</p>
                </Col>
                <Col :span="4">
                <p>状态:</p>
                </Col>
                <Col :span="8">
                <p>{{detailModal.current.state?'已审核':'未审核' }}</p>
                </Col>
            </Row>
            <Row>
                <Col :span="4">
                <p>营业执照:</p>
                </Col>
                <Col :span="8">
                <img class="img" :src="detailModal.current.licenseUrl" />
                </Col>
                <Col :span="4">
                <p>法人证明:</p>
                </Col>
                <Col :span="8">
                <img class="img" :src="detailModal.current.identityCardUrl" />
                </Col>
            </Row>
        </Modal>

        <!-- 审核窗口 -->
        <Modal :transfer='false' v-model='auditModal.isShow' :title='auditModal.title' :mask-closable='false' @on-ok='commitAudit' @on-cancel='cancel'>
            <Form ref="audit" :model="auditModal.current" inline :label-width="120">
                <Row>
                    <Col>
                    <FormItem label="审核状态">
                        <i-switch v-model="auditModal.current.state" size="large">
                        </i-switch>
                    </FormItem>
                    </Col>
                </Row>
                <Row>
                    <Col>
                    <FormItem v-if="!auditModal.current.state" label="审核意见" prop="name">
                        <Input v-model="auditModal.current.opinion" type="textarea" :autosize="{minRows: 2,maxRows: 5}" style="width:300px;" placeholder="请输入..."></Input>
                    </FormItem>
                    </Col>
                </Row>

            </Form>
        </Modal>

        <!-- 充值窗口 -->
        <Modal :transfer='false' v-model='chargeModal.isShow' :title='chargeModal.title' :mask-closable='false' @on-ok='commitCharge' @on-cancel='cancel'>
            <Form ref="charge" :model="chargeModal.current" inline :label-width="120">
                <FormItem label="充值金额" prop="name">
                    <InputNumber  v-model="chargeModal.current.money" placeholder="充值金额"></InputNumber>
                </FormItem>
            </Form>
        </Modal>
        <!-- 重置密码窗口 -->
        <Modal :transfer='false' v-model='resetModal.isShow' :title='resetModal.title' :mask-closable='false' @on-ok='commitReset' @on-cancel='cancel'>
            <Form ref="charge" :model="resetModal.current" inline :label-width="120">
                <FormItem label="新密码" prop="password">
                    <Input type="password" v-model="resetModal.current.password" placeholder="新密码"></Input>
                </FormItem>
            </Form>
        </Modal>

    </div>
</template>

<script>
import {
  getCustomers,
  deleteCustomer,
  auditCustomer,
  resetCustomer,
  getCustomer
} from "api/customer";
import { chargeCustomer } from "api/record";
export default {
  name: "customer",
  data() {
    return {
      cols: [
        {
          title: "公司名称",
          key: "companyName"
        },
        {
          title: "联系人",
          key: "contact"
        },
        {
          title: "所在城市",
          key: "city"
        },
        {
          title: "余额",
          key: "balance"
        },
        {
          title: "详细地址",
          key: "address"
        },
        {
          title: "状态",
          key: "state",
          render: (h, params) => {
            return params.row.state ? "已审核" : "未审核";
          }
        },
        {
          title: "操作",
          key: "action",
          align: "center",
          width: "250px",
          render: (h, params) => {
            let children = [];
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
                      this.detail(params.row);
                    }
                  }
                },
                "详情"
              )
            ); //组件1
            children.push(
              h(
                "Button",
                {
                  props: {
                    type: "primary",
                    size: "small"
                  },
                  style: {
                    marginRight: "5px",
                    hidden: !params.row.state
                  },
                  on: {
                    click: () => {
                      this.delete(params.row);
                    }
                  }
                },
                "删除"
              )
            ); //组件3
            if (!params.row.state) {
              children.push(
                h(
                  "Button",
                  {
                    props: {
                      type: "primary",
                      size: "small"
                    },
                    style: {
                      marginRight: "5px",
                      hidden: params.row.state
                    },
                    on: {
                      click: () => {
                        this.audit(params.row);
                      }
                    }
                  },
                  "审核"
                )
              ); //组件3
            } else {
              children.push(
                h(
                  "Button",
                  {
                    props: {
                      type: "error",
                      size: "small"
                    },
                    style: {
                      marginRight: "5px",
                      hidden: params.row.state
                    },
                    on: {
                      click: () => {
                        this.charge(params.row);
                      }
                    }
                  },
                  "充值"
                )
              ); //组件2

              children.push(
                h(
                  "Button",
                  {
                    props: {
                      type: "primary",
                      size: "small"
                    },
                    style: {
                      marginRight: "5px",
                      hidden: params.row.state
                    },
                    on: {
                      click: () => {
                        this.pricing(params.row);
                      }
                    }
                  },
                  "定价"
                )
              ); //组件2

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
                        this.reset(params.row);
                      }
                    }
                  },
                  "重置密码"
                )
              ); //组件2
            }

            return h("div", children);
          }
        }
      ],
      searchApi: getCustomers,
      params: { companyName: "", contact: "", city: "" },
      detailModal: {
        isShow: false,
        title: "客户详情",
        current: {}
      },
      auditModal: {
        isShow: false,
        title: "客户审核",
        current: { state: true, opinion: "", id: null }
      },
      chargeModal: {
        isShow: false,
        title: "客户充值",
        current: { money: 0, id: null }
      },
      resetModal: {
        isShow: false,
        title: "密码重置",
        current: { password: 0, id: null }
      }
    };
  },
  components: {},
  created() {
    this.$root.eventHub.$on("customer", () => {
      this.cancel();
    });
  },
  destroyed() {
    this.$root.eventHub.$off("customer");
  },
  methods: {
    // 删除
    delete(model) {
      const table = this.$refs.list;
      this.$Modal.confirm({
        title: "删除提示",
        content: "确定要删除当前客户么?",
        onOk: () => {
          const parms = { id: model.id };
          deleteCustomer(parms).then(c => {
            if (c.data.success) {
              table.initData();
            }
          });
        }
      });
    },
    pricing(row) {
      this.$router.push({
        path: "customerpricing",
        query: { customerId: row.id }
      });
    },
    reset(row) {
      this.resetModal.isShow = true;
      this.resetModal.title = this.resetModal.title + ": " + row.companyName;
      this.resetModal.current.id = row.id;
    },
    detail(row) {
      getCustomer({ id: row.id })
        .then(r => {
          if (r.data.success) {
            this.detailModal.current = r.data.result;
            this.detailModal.isShow = true;
            this.detailModal.title =
              this.detailModal.title + ": " + row.companyName;
          } else {
            this.$Message.warn("获取信息失败");
          }
        })
        .catch(e => {
          this.$Message.error(e.message);
        });
    },
    audit(row) {
      this.auditModal.isShow = true;
      this.auditModal.title = this.auditModal.title + ": " + row.companyName;
      this.auditModal.current.id = row.id;
    },
    charge(row) {
      this.chargeModal.isShow = true;
      this.chargeModal.title = this.chargeModal.title + ": " + row.companyName;
      this.chargeModal.current.id = row.id;
    },
    commitCharge() {
      chargeCustomer(this.chargeModal.current)
        .then(r => {
          if (r.data.success) {
            this.$refs.list.initData();
          }
        })
        .catch(e => {
          this.$Message.error(e.message);
        });
    },
    commitReset() {
      resetCustomer(this.resetModal.current)
        .then(r => {
          if (r.data.success) {
            this.$refs.list.initData();
          }
        })
        .catch(e => {
          this.$Message.error(e.message);
        });
    },
    commitAudit() {
      auditCustomer(this.auditModal.current)
        .then(r => {
          if (r.data.success) {
            this.$refs.list.initData();
          }
        })
        .catch(e => {
          this.$Message.error(e.message);
        });
    },
    save() {
      this.$refs.product.commit();
    },
    cancel() {
      this.auditModal.isShow = false;
      this.auditModal.title = "客户审核";

      this.chargeModal.isShow = false;
      this.chargeModal.title = "客户充值";

      this.resetModal.isShow = false;
      this.resetModal.title = "密码重置";

      this.detailModal.isShow = false;
      this.detailModal.title = "客户详情";
      this.$refs.list.initData();
    }
  }
};
</script>

<style type='text/css' scoped>
.img {
  width: 70%;
}
</style>