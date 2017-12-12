<template>
    <div class='animated fadeIn'>
        <Row>
            <milk-table ref='list' :layout='[18,3,3]' :columns='cols' :search-api='searchApi' :params='params'>
                <template slot='search'>
                    <Form ref='params' :model='params' inline :label-width='60'>
                        <FormItem label='产品名称'>
                            <Input v-model='params.name' placeholder='请输入产品名称'></Input>
                        </FormItem>
                        <FormItem label='状态'>
                            <Select v-model='params.state'>
                                <Option value="">全部</Option>
                                <Option value="true">上架</Option>
                                <Option value="false">下架</Option>
                            </Select>
                        </FormItem>
                        <FormItem label='一级分类'>
                            <Select @on-change="pick" v-model='params.levelOne' style="width:120px;">
                                <Option value="">全部</Option>
                                <Option v-for='c in Ones' :value='c.id' :key='c.id'>{{c.name}}</Option>
                            </Select>
                        </FormItem>
                        <FormItem label='二级分类'>
                            <Select v-model='params.levelTwo' style="width:120px;">
                                <Option value="">全部</Option>
                                <Option v-for='c in Twos' :value='c.id' :key='c.id'>{{c.name}}</Option>
                            </Select>
                        </FormItem>
                    </Form>
                </template>
                <template slot='actions'>
                    <Button @click='add' type='primary'>添加</Button>
                </template>
            </milk-table>
        </Row>
        <!-- 添加和编辑窗口 -->
        <Modal ref="modal" :loading="modal.isloading" :width='800' :transfer='false'
         v-model='modal.isEdit'
         :title='modal.title' 
         :mask-closable='false'
          @on-ok='save' @on-cancel='cancel'>
            <modify-product ref='product' :productId='modal.current' v-if='modal.isEdit' />
        </Modal>

    </div>
</template>

<script>
import { getProducts, deleteProduct, UpdateState } from "api/products";
import modifyProduct from "./modify-product";
import { getCates } from "api/cate";
export default {
  name: "account",
  data() {
    return {
      cols: [
        {
          type: "selection",
          align: "center",
          width: "70px"
        },
        {
          title: "商品名称",
          key: "productName"
        },

        {
          title: "创建时间",
          key: "creationTime",
          render: (h, params) => {
            return this.$fmtTime(params.row.creationTime);
          }
        },
        {
          title: "一级分类",
          key: "levelOneName"
        },
        {
          title: "二级分类",
          key: "levelTwoName"
        },
        {
          title: "状态",
          key: "isActive",
          render: (h, params) => {
            return params.row.isActive ? "上架" : "下架";
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
                      this.edit(params.row);
                    }
                  }
                },
                "修改"
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
                    marginRight: "5px"
                  },
                  on: {
                    click: () => {
                      this.pricing(params.row);
                    }
                  }
                },
                "定价"
              )
            ); //组件1
            if (params.row.isActive) {
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
                        this.state(params.row);
                      }
                    }
                  },
                  "下架"
                )
              ); //组件3
            } else {
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
                        this.state(params.row);
                      }
                    }
                  },
                  "上架"
                )
              ); //组件3
            }
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
            return h("div", children);
          }
        }
      ],
      searchApi: getProducts,
      params: { name: "", state: null, levelOne: null, levelTwo: null },
      modal: {
        isEdit: false,
        title: "添加",
        current: null,
        isloading: true
      },
      Ones: [],
      Twos: []
    };
  },
  components: {
    modifyProduct
  },
  created() {
    this.$root.eventHub.$on("product", result => {
      debugger;
      if(result){
      this.cancel();

      }else{
        this.changeLoading();
      }
    });
    this.initCates(null);
  },
  destroyed() {
    this.$root.eventHub.$off("product");
  },
  methods: {
    pick() {
      getCates({ id: this.params.levelOne }).then(c => {
        if (c.data.success && c.data.result) {
          this.Twos = c.data.result;
        }
      });
    },
    pricing(row) {
      this.$router.push({ path: "pricing", query: { productId: row.id } });
    },
    state(model) {
      const table = this.$refs.list;
      UpdateState({ id: model.id }).then(r => {
        if (r.data.success) {
          table.initData();
        }
      });
    },
    changeLoading() {
      this.modal.isloading = false;
      this.$nextTick(() => {
        this.modal.isloading = true;
      });
    },
    // 删除
    delete(model) {
      const table = this.$refs.list;
      this.$Modal.confirm({
        title: "删除提示",
        content: "确定要删除当前产品么?",
        onOk: () => {
          const parms = { id: model.id };
          deleteProduct(parms).then(c => {
            if (c.data.success) {
              table.initData();
            }
          });
        }
      });
    },
    initCates(current) {
      getCates({ id: current }).then(c => {
        if (c.data.success && c.data.result) {
          if (current == null) {
            this.Ones = c.data.result;
          } else {
            this.Twos = c.data.result;
          }
        }
      });
    },
    add() {
      this.modal.isEdit = true;
      this.modal.title = "添加商品";
    },
    edit(row) {
      this.modal.current = row.id;
      this.modal.isEdit = true;
      this.modal.title = "编辑商品:" + row.productName;
    },
    save() {
      this.$refs.product.commit();
    },
    cancel() {
       this.changeLoading();
      this.modal.isEdit = false;
      this.modal.title = "添加用户";
      this.modal.current = null;
      this.$refs.list.initData();
    },
    changeState() {}
  }
};
</script>

<style type='text/css' scoped>

</style>