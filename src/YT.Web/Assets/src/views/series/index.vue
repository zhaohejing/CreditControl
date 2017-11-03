<template>
    <div class="animated fadeIn">
        <Row>
            <Col :span="3">一级分类</Col>
            <Col :span="5">
            <Select @on-change="pickLevelOne" v-model="levelOne" style="width:200px">
                <Option v-for="item in LevelOneList" :value="item.id" :key="item.id">{{ item.name }}</Option>
            </Select>
            </Col>
            <Col :span="1">
            <Button v-if="levelOne" @click="deleteCateOne">
                <Icon type="close" />
            </Button>
            </Col>
            <Col :span="8">
            <Button @click="addLevelOne">添加一级分类</Button>
            </Col>
        </Row>
        <Row>
            <Col :span="3">二级分类</Col>
            <Col :span="5">
            <Select v-model="levelTwo" style="width:200px">
                <Option v-for="item in LevelTwoList" :value="item.id" :key="item.id">{{ item.name }}</Option>
            </Select>
            </Col>
            <Col :span="1">
            <Button v-if="levelTwo" @click="deleteCateTwo">
                <Icon type="close" />
            </Button>
            </Col>
            <Col :span="8">
            <Button @click="addLevelTwo">添加二级分类</Button>
            </Col>
        </Row>
        <!-- 添加和编辑窗口 -->
        <Modal :transfer='false' v-model='modal.isEdit' :title='modal.title' :mask-closable='false' @on-ok='save' @on-cancel='cancel'>
            <Form ref="cate" :model="modal.current" :rules="ruleValidate" inline :label-width="120">
                <FormItem label="分类名称" prop="name">
                    <Input v-model="modal.current.name" placeholder="分类名称"></Input>
                </FormItem>
            </Form>
        </Modal>
    </div>
</template>

<script>
import {
  getPageCates,
  getCates,
  deleteCate,
  deleteCates,
  getCateForEdit,
  modifyCate
} from "api/cate";
export default {
  name: "series",
  data() {
    return {
      levelOne: null,
      levelTwo: null,
      LevelOneList: [],
      LevelTwoList: [],
      modal: {
        isEdit: false,
        title: "添加一级分类",
        current: { id: null, name: "", parentId: null }
      },
      ruleValidate: {
        name: [{ required: true, message: "姓名不可为空", trigger: "blur" }]
      }
    };
  },
  created() {
    this.initCates();
  },
  methods: {
    deleteCateOne() {
      this.$Modal.confirm({
        title: "删除提示",
        content: "确定要删除一级分类么?",
        onOk: () => {
          deleteCate({ id: this.levelOne })
            .then(r => {
              if (r.data.success) {
                this.initCates();
              }
            })
            .catch(e => {
              this.$Message.error(e.response.data.error.message);
            });
        }
      });
    },
    deleteCateTwo() {
      this.$Modal.confirm({
        title: "删除提示",
        content: "确定要删除二级分类么?",
        onOk: () => {
          deleteCate({ id: this.levelTwo })
            .then(r => {
              if (r.data.success) {
                this.pickLevelOne(this.levelOne);
              }
            })
            .catch(e => {
              this.$Message.error(e.response.data.error.message);
            });
        }
      });
    },
    addLevelOne() {
      this.modal.isEdit = true;
      getCateForEdit({ id: null }).then(r => {
        if (r.data.result) {
          this.modal.current = r.data.result;
        }
      });
    },
    addLevelTwo() {
      if (!this.levelOne) {
        this.$Message.error("请选择一级分类!");
        return;
      }
      this.modal.isEdit = true;
      getCateForEdit({ id: null }).then(r => {
        if (r.data.result) {
          this.modal.current = r.data.result;
          this.modal.current.parentId = this.levelOne;
        }
      });
    },
    pickLevelOne(current) {
      getCates({ id: current }).then(r => {
        if (r.data.result) {
          this.LevelTwoList = r.data.result;
        }
      });
    },
    initCates() {
      getCates({ id: null }).then(r => {
        if (r.data.result) {
          this.LevelOneList = r.data.result;
        }
      });
    },
    save() {
      this.$refs.cate.validate(valid => {
        if (valid) {
          modifyCate({ categoryEditDto: this.modal.current }).then(r => {
            if (r.data.success) {
              this.initCates();
            }
          });
        } else {
          this.$Message.error("请完善表单数据!");
          return;
        }
      });
    },
    cancel() {
      this.initCates();
    }
  }
};
</script>


<style type="text/css" scoped>

</style>