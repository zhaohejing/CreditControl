<template>
  <div class='contentMain'>
    <Breadcrumb class='breadHead'>
      <BreadcrumbItem>公告通知</BreadcrumbItem>
    </Breadcrumb>
    <div class='g-mianBox'>
      <Row @click.native="details(item)" class='g-bm' v-for='item in result' :key='item.id'>
        <div class='g-width'>
          <Col span='12' class='g-left'>
          <span>{{item.title}}</span>
          </Col>
          <Col span='12' class='g-right'>
          <span>{{item.creationTime | formatDate}}</span>
          </Col>
        </div>
      </Row>
      <Row v-if='result.length == 0' style='padding:50px 0'>
        暂无相关产品
      </Row>
    </div>
  </div>
</template>

<script>
import { infos, info } from "api/public";
import { parseTime } from "utils/index";
export default {
  data() {
    return {
      result: [],
      params: {
        filter: "",
        maxResultCount: 10,
        skipCount: 0
      }
    };
  },
  // 过滤器
  filters: {
    formatDate(time) {
      return parseTime(time);
    }
  },
  created() {
    this.init();
  },
  methods: {
    init() {
      infos(this.params).then(r => {
        if (r.data.success) {
          this.result = r.data.result.items;
        }
      });
    },
    // 查看详情
    details(item) {
      this.$router.push({
        path: "/ainfo/" + item.id
      });
    }
  }
};
</script>

<style rel='stylesheet/scss' lang='scss'>
.g-left {
  text-align: left;
}

.g-right {
  text-align: right;
}

.contentMain {
  width: 100%;
  height: 100%;
  padding: 32px 65px 32px 65px;
  .breadHead {
    padding-bottom: 32px;
  }
  .ivu-breadcrumb > span:last-child {
    font-weight: normal;
    color: #4d4d4d;
    font-size: 14px;
    height: 14px;
    line-height: 14px;
    border-left: 2px solid #8eb7ef;
    padding-left: 10px;
  }
  .g-mianBox {
    width: 100%;
    padding: 0 10px;
    box-shadow: 0px 0px 7px 4px rgba(1, 34, 63, 0.1);
    .g-bm {
      border-bottom: 1px solid #ebebeb;
      padding: 10px 0;
      .g-rightFootTag {
        margin-top: 16px;
      }
    }
  }
}
</style>
