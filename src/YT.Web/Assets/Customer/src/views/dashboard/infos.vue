<template>
    <div class='contentMain'>
        <Breadcrumb class='breadHead'>
            <BreadcrumbItem>公告通知</BreadcrumbItem>
        </Breadcrumb>
        <div class='g-mianBox'>
            <Row class='g-bm' v-for='item in result' :key='item.id'>
                <div class='g-width'>
                <Col span='12' class='g-center'>
                    <span>{{item.title}}</span>
                </Col>
                 <Col span='12' class='g-center'>
                    <span>{{item.title}}</span>
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
        path: "/detail",
        params: { id: item.id }
      });
    }
  }
};
</script>

<style rel='stylesheet/scss' lang='scss'>
.g-center {
  text-align: center;
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
      margin-bottom: 37px;
      .g-width {
        span {
          display: inline-block;
          width: 120px;
          height: 30px;
          background: #ccddf4;
          font-size: 14px;
          color: #679feb;
          text-align: center;
          line-height: 30px;
          margin-top: 16px;
        }
      }
      h2 {
        font-size: 15px;
        font-weight: normal;
        padding-bottom: 16px;
      }
      p {
        font-size: 14px;
        color: #999;
        line-height: 26px;
        border-bottom: 1px solid #ebebeb;
        padding-bottom: 20px;
        margin-bottom: 16px;
        max-height: 85px;
        overflow: hidden;
        text-overflow: ellipsis;
        display: -webkit-box;
        -webkit-line-clamp: 3;
        -webkit-box-orient: vertical;
      }
      .g-rightFootTag {
        margin-top: 16px;
        .lookDetail {
          width: 82px;
          height: 26px;
          background: #4f5873;
          text-align: center;
          line-height: 26px;
          padding: 0;
          border: 1px solid #4f5873;
          border-radius: 0;
        }
        .singlePrice {
          display: inline-block;
          width: 102px;
          height: 26px;
          border: 1px solid #ebebeb;
          color: #ccc;
          text-align: center;
          line-height: 26px;
          font-size: 12px;
          vertical-align: middle;
          margin: 0 26px;
        }
        .unitPrice {
          width: 120px;
          height: 26px;
          line-height: 26px;
          display: inline;
        }
        .g-add {
          width: 28px;
          height: 26px;
          line-height: 26px;
          text-align: center;
          padding: 0;
          color: #ccc;
          border: 1px solid #ebebeb;
          border-radius: 0;
        }
        .order {
          width: 102px;
          height: 26px;
          background: #4f5873;
          text-align: center;
          line-height: 26px;
          padding: 0;
          border: 1px solid #4f5873;
          border-radius: 0;
          margin-left: 26px;
        }
        .input input {
          height: 26px;
          text-align: center;
          line-height: 26px;
          border-radius: 0;
          border-left: none;
          border-right: none;
        }
      }
    }
    .g-bm:last-child {
      border-bottom: none;
    }
  }
}
</style>
