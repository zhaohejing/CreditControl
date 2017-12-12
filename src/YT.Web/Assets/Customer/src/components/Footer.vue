<template>
  <div class="footer">
    <span class="gl-24">{{ footBag }}</span>
    {{ footICP }}
  </div>
</template>

<script>
export default {
  name: "footer",
  data() {
    return {
      footBag: "北京视界奇点文化传媒有限公司版权所有",
      footICP: "京ICP备16050160号-1"
    };
  }
};
</script>

<style rel="stylesheet/scss" lang="scss" scoped>
.footer {
  height: 42px;
  line-height: 42px;
  text-align: center;
  background: #383d41;
  color: #fff;
  .gl-24 {
    padding-right: 24px;
  }
}
</style>
