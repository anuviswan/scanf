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


const getAllItemsForCategory = async (category)=>{

    const params ={
        params:{
            appName:'scanf',
            categoryName:category
        }
    };

    const response = await axios.get('http://localhost:7071/api/item/getall',params);
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