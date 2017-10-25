<template>
  <div class="sidebar">
    <nav class="sidebar-nav">
      <ul class="nav">
        <template v-for="item in routes">
          <router-link :key="item.name" tag="li" class="nav-item nav-dropdown" v-if="item.children&&item.children.length>0"
             to="" disabled exact>
            <div class="nav-link nav-dropdown-toggle"  @click="handleClick">
              <Icon :type="item.icon" />{{ item.displayName}} </div>
            <ul class="nav-dropdown-items">
              <li class="nav-item" :key="child.name" v-for="child in item.children" @click="addActive">
                <router-link :to="child.url" class="nav-link">
                  <Icon :type="child.icon" />{{ child.displayName}} </router-link>
              </li>
            </ul>
          </router-link>
          
          <li class="nav-item" :key="item.name" v-if="!item.children||item.children.length<=0">
            <router-link :to="item.url" class="nav-link" exact>
              <Icon :type="item.icon" />{{ item.displayName}} </router-link>
          </li>

        </template>
      </ul>
    </nav>
  </div>
</template>

<script>

export default {
  name: 'SidebarItem',
  props: {
    routes: {
      type: Array
    }
  },
  data() {
    return {

    }
  },
  methods: {
    handleClick(e) {
      e.preventDefault()
      e.target.parentElement.classList.toggle('open')
    },
    addActive(e) {
      e.preventDefault()
      e.target.parentElement.parentElement.parentElement.classList.add('open')
    }
  },
  created() {

  },
  mounted() {
  }
}
</script>

<style rel="stylesheet/scss" lang="scss" scoped>
.wscn-icon {
  margin-right: 10px;
}

.hideSidebar .menu-indent {
  display: block;
  text-indent: 10px;
}
</style>

