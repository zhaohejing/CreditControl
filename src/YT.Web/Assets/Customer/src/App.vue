<template>
  <div id="app">
    <router-view/>
  </div>
</template>

<script>
export default {
  name: "app"

};
</script>

<style>
#app {
  height: 100%;
}

html,
body {
  height: 100%;
  overflow-x: hidden;
  overflow-y: auto;
  font-family: "Microsoft Yahei";
}
</style>
