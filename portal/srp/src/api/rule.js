import axios from 'axios'


const getAllRuleCategories = async ()=>{

    const params = {
        params: {
            appName:'scanf'
        },
      };
     
    const response = await axios.get(process.env.VUE_APP_APIGETALLCATEGORIES,params);
    const result = response.data.map(item => item.rowKey);
    return result;
};


const getAllItemsForCategory = async (category)=>{

    const params ={
        params:{
            appName:'scanf',
            categoryName:category
        }
    };
    console.log(process.env.VUE_APP_APIGETALLITEMS);
    console.log(process.env.VUE_APP_APIGETALLCATEGORIES);
    const response = await axios.get(process.env.VUE_APP_APIGETALLITEMS,params);
    const result = response.data.map(item=>{
        const serializedData = JSON.parse(item.value);

        return {
            title: serializedData.title,
            description: serializedData.descriptionPath,
            category: category
        };
    });

    return result;
}

const getRuleDescription = async (url)=>{
    const response = await axios.get(url);
    console.log(response.data.content);
    return response.data.content;
}

export {
    getAllRuleCategories, getAllItemsForCategory,getRuleDescription
};