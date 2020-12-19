<template>
  <div class="container-fluid">
    <div class="row">
      <div class="col-12">
        <Navbar />
      </div>
    </div>
    <div class="row">
      <div class="col-3">
        <RuleList />
      </div>
    </div>
  </div>
</template>

<script>
import Navbar from "./generic/Navbar";
import RuleList from "./generic/RuleList";
export default {
  name: "Shell",
  components: {
    Navbar,
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
