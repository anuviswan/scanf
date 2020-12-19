<template>
  <v-container>
    <v-row>
      <v-col cols="3" align-self="start"><RuleList /></v-col>
      <v-col cols="9"><v-card>Rule Description</v-card></v-col>
    </v-row>
  </v-container>
</template>

<script>
import RuleList from "./rules/RuleList";
export default {
  name: "Shell",
  components: {
    RuleList,
  },
  props: {
    msg: String,
  },
};
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>
h3 {
  margin: 40px 0 0;
}
ul {
  list-style-type: none;
  padding: 0;
}
li {
  display: inline-block;
  margin: 0 10px;
}
a {
  color: #42b983;
}
</style>
