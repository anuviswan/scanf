import axios from 'axios'


const getAllRuleCategories = async ()=>{

    const params = {
        params: {
            appName:'scanf'
        },
      };
    // console.log(process.env.VUE_APP_ID);
    const response = await axios.get(' http://localhost:7071/api/category/getall',params);
    const result = response.data.map(item => item.rowKey);
    return result;

};

export {
    getAllRuleCategories
};