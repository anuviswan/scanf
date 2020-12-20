<template>
  <div>
    <v-list-item :key="item.title" two-line>
      <v-list-item-content class="float-left">
        <v-list-item-title>{{ item.rowKey }}</v-list-item-title>
        <v-list-item-subtitle>{{ item.category }} </v-list-item-subtitle>
      </v-list-item-content>
    </v-list-item>
    <v-divider />
  </div>
</template>

<script>
export default {
  name: "RuleItem",
  props: {
    item: {},
  },
  created() {
    console.log(this.item);
  },
};
</script>

<style></style>
