<template>
  <div>
    <v-list-item :key="item.title" two-line @click="onClick(item)">
      <v-list-item-icon>
        <v-icon small>fas fa-code</v-icon>
      </v-list-item-icon>
      <v-list-item-content class="float-left">
        <v-list-item-title>{{ item.title }}</v-list-item-title>
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
  methods: {
    onClick(itemClicked) {
      console.log(itemClicked);
      this.$emit("onItemClicked", itemClicked.description);
    },
  },
};
</script>

<style></style>
