<template>
    <div class="animated fadeIn">
        <Row>
            <Col :span="8"> 单号:{{order.orderNum }}
            </Col>
            <Col :span="8"> 状态:{{order.state==null?'未完成':(order.state?'已完成':'已取消') }}
            </Col>
            <Col :span="8">
            <a @click="viewForm" v-if="order.formId">查看表单</a>
            </Col>
        </Row>
        <Row>
            <table class="gridtable">
                <thead>
                    <tr>
                        <th>商品名称</th>
                        <th>单价</th>
                        <th>个数</th>
                        <th>总价</th>
                    </tr>
                </thead>
                <tbody>
                    <tr>
                        <td>{{order.productName}}</td>
                        <td>{{order.price}}</td>
                        <td>{{order.count}}</td>
                        <td>{{order.totalPrice}}</td>
                    </tr>
                </tbody>
            </table>
        </Row>
        <Row>
            <Col :span="8"> 公司名称:{{order.companyName }}
            </Col>
            <Col :span="8"> 联系人名称:{{order.contact }}
            </Col>
            <Col :span="8"> 商品总价:{{order.totalPrice }}
            </Col>
        </Row>
        <Row v-if="order.state!=null&&!order.state">
            <Col :span="2"> 订单取消原因
            </Col>
            <Col :span="22">
            <Input v-model="order.cancelReason" disabled placeholder=""></Input>
            </Col>
        </Row>
        <Row>
            <Col :span="4" offset="9">
            <Button type="ghost" @click="back" style="margin-left: 8px">返回</Button>
            <Button v-if="order.state" type="primary" @click="payback">退款</Button>
            </Col>
        </Row>
        <!-- 添加和编辑窗口 -->
        <Modal :width='800' :transfer='false' v-model='modal.isShow' :title='modal.title' ok-text="导出" :mask-closable='false' @on-ok='save'
            @on-cancel='cancel'>
            <Form ref='form' :model='form' inline>
                <div class="infoBox">
                    <Row class='basic'>
                        <Col span='22'>公司基本信息</Col>
                        <Col span='2'>
                        <Button type="primary" @click="save">导出</Button>
                        </Col>
                    </Row>
                    <div class="infoInput">
                        <FormItem label="公司名称" prop='companyName'>
                            <Input v-model="form.companyName"></Input>
                        </FormItem>
                        <FormItem label="所属行业">
                            <Input v-model="form.industry"></Input>
                        </FormItem>
                        <FormItem label="品牌名称">
                            <Input v-model="form.brands"></Input>
                        </FormItem>
                        <FormItem label="法人代表">
                            <Input v-model="form.legalPerson"></Input>
                        </FormItem>
                        <FormItem label="法人代表手机号码">
                            <Input v-model="form.legalMobile"></Input>
                        </FormItem>
                        <FormItem label="品牌负责人">
                            <Input v-model="form.brandsPerson"></Input>
                        </FormItem>
                        <FormItem label="品牌负责人手机号码">
                            <Input v-model="form.brandsMobile"></Input>
                        </FormItem>
                        <FormItem label="联系地址">
                            <Input v-model="form.address"></Input>
                        </FormItem>
                        <FormItem label="邮编">
                            <Input v-model="form.postNum"></Input>
                        </FormItem>
                        <FormItem label="电子邮箱" prop='email'>
                            <Input v-model="form.email"></Input>
                        </FormItem>
                    </div>
                </div>
                <div class="infoBox">
                    <Row class='basic'>
                        <Col span='24'>资质上传</Col>
                    </Row>
                    <Row class='fileUpload'>
                        <Row>
                            <Col span="9">
                            <template class="demo-upload-list" v-if="form.license">
                                <a :href="form.licenseUrl" download="img">
                                    <img style="width:200px;height:150px;" :src="form.licenseUrl">
                                </a>
                            </template>
                            <p class='g9b9ea0 g-center'>企业有效期营业执照复本电子版上传</p>
                            </Col>
                        </Row>
                        <Row style="margin-top:45px">
                            <Col span="9">
                            <template class="demo-upload-list" v-if="form.bottomIdCard">
                                <a :href="form.bottomIdCardUrl" download="img">
                                </a>
                                <img style="width:200px;height:150px;" :src="form.bottomIdCardUrl">
                            </template>
                            <p class='g9b9ea0 g-center'>企业法人身份证反面电子版上传</p>
                            </Col>
                            <Col span="9" offset="6">
                            <template class="demo-upload-list" v-if="form.topIdCard">
                                <a :href="form.topIdCardUrl" download="img">
                                    <img style="width:200px;height:150px;" :src="form.topIdCardUrl">
                                </a>
                            </template>
                            <p class='g9b9ea0 g-center'>企业法人身份证正面电子版上传</p>
                            </Col>
                        </Row>
                        <Row v-if="type1">
                            <Col span="6"> 企业所属行业特有许可证或企业荣誉
                            </Col>
                            <Col span="12">
                            <Row >
                                <Col v-for="(item,index) in type1" :span="6">
                                <template class="demo-upload-list" v-if="item.profileUrl">
                                    <a :href="item.profileUrl" download="img">
                                        <img style="width:200px;height:150px;" :src="item.profileUrl">
                                    </a>
                                </template>
                                </Col>
                            </Row>
                            </Col>
                        </Row>

                          <Row v-if="type2">
                            <Col span="6"> 相关附件
                            </Col>
                            <Col span="12">
                            <Row >
                                <Col v-for="(item,index) in type2" :span="6">
                                <template class="demo-upload-list" v-if="item.profileUrl">
                                    <a :href="item.profileUrl" download="img">
                                        <img style="width:200px;height:150px;" :src="item.profileUrl">
                                    </a>
                                </template>
                                </Col>
                            </Row>
                            </Col>
                        </Row>
                    </Row>
                </div>

            </Form>
        </Modal>
    </div>
</template>

<script>
import { order, exportData, payBack, getFormByOrder } from "api/products";
export default {
  name: "orderdetail",
  data() {
    return {
      order: {},
      form: {},
      modal: {
        isShow: false,
        title: "表单详情"
      }
    };
  },
  created() {
    this.init();
  },
  computed: {
    type1() {
      if (this.form.formProfiles) {
        return this.form.formProfiles.filter(function(x) {
          return x.profileType == 1;
        });
      } else return null;
    },
    type2() {
      if (this.form.formProfiles) {
        return this.form.formProfiles.filter(function(x) {
          return x.profileType == 2;
        });
      } else return null;
    }
  },
  destroyed() {},
  methods: {
    init() {
      order({
        id: this.$route.query.id
      })
        .then(r => {
          if (r.data.success) {
            this.order = r.data.result;
          }
        })
        .catch(e => {
          this.$Message.error(e.response.data.error.message);
        });
    },
    // 完成
    payback() {
      this.$Modal.confirm({
        title: "操作提示",
        content: "确定要退款么?",
        onOk: () => {
          const parms = {
            id: this.order.id
          };
          payBack(parms)
            .then(c => {
              if (c.data.success) {
                this.$Message.success("提交成功");
                this.$router.push({
                  path: "/orders"
                });
              }
            })
            .catch(e => {
              this.$Message.error(e.response.data.error.message);
            });
        }
      });
    },
    back() {
      this.$router.push({
        path: "/orders"
      });
    },
    save() {
      window.open(
        "http://47.93.2.82:9999/api/File/DownLoadFile?orderId=" + this.order.id
      );

      this.modal.isShow = false;
    },
    cancel() {
      this.modal.isShow = false;
    },
    viewForm() {
      getFormByOrder({
        id: this.order.id
      }).then(r => {
        if (r.data.success) {
          this.form = r.data.result;
          this.modal.isShow = true;
        }
      });
    }
  }
};
</script>


<style type="text/css" scoped>
table.gridtable {
  width: 100%;
  font-family: verdana, arial, sans-serif;
  font-size: 11px;
  color: #333333;
  border-width: 1px;
  border-color: #666666;
  border-collapse: collapse;
}

table.gridtable th {
  border-width: 1px;
  padding: 8px;
  border-style: solid;
  border-color: #666666;
  background-color: #dedede;
}

table.gridtable td {
  border-width: 1px;
  padding: 8px;
  border-style: solid;
  border-color: #666666;
  background-color: #ffffff;
}
</style>