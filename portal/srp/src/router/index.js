import Vue from "vue"
import VueRouter from "vue-router"
import Shell from "../components/Shell"
import Vuetify from "vuetify";
import "vuetify/dist/vuetify.min.css";

Vue.use(Vuetify);

Vue.use(VueRouter)

const routes = [
{
    path:"/",
    name:"Default",
    component:Shell,
}
]

const router = new VueRouter({
    routes
});


export default router;


