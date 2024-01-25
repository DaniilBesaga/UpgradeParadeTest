import { useEffect, useState } from 'react';
import './MainPageStyle.css';
import Cart from './Cart';
interface Prod {
    productPrice: number;
    productName: string;
    productImg: string;
}

function MainPage() {
    const [prs, setPrs] = useState<Prod[]>();

    useEffect(() => {
        displayProducts();
    }, []);
    const [quantities, setQuantities] = useState({});

    const [showCart, setShowCart] = useState(false);

    const handleClick = () => {
        setShowCart(!showCart);
    };

    const incrementQuantity = (productName) => {
        setQuantities((prevQuantities) => {
            const prevQuantity = prevQuantities[productName] || 0;
            return {
                ...prevQuantities,
                [productName]: prevQuantity + 1,
            };
        });
    };


    const decrementQuantity = (productName) => {
        setQuantities((prevQuantities) => {
            const prevQuantity = prevQuantities[productName] || 0;
            if (prevQuantity > 0) {
                return {
                    ...prevQuantities,
                    [productName]: prevQuantity - 1,
                };
            }
            return prevQuantities;
        });
    };


    const calculateSum = () => {
        let sum = 0;
        if (prs != undefined) {
            prs.forEach((pr) => {
                const quantity = quantities[pr.productName] || 0;
                sum += pr.productPrice * quantity;
            });
        }
        return sum;
    };
    const contents = prs === undefined
        ? <p><em>Loading... Please refresh once the ASP.NET backend has started. See <a href="https://aka.ms/jspsintegrationreact">https://aka.ms/jspsintegrationreact</a> for more details.</em></p>
        : <table className="table table-striped">
            <thead>
                <tr>
                    <th>Product</th>
                    <th>Name</th>
                    <th>Price (UAH)</th>
                    
                </tr>
            </thead>
            <tbody>
                {prs.map(pr =>
                    <tr key={pr.productName}>
                        <td>
                            <img src={pr.productImg}></img>
                        </td>
                        <td><p>{pr.productName}</p></td>   
                        <td><p>{pr.productPrice}</p></td>
                        <td><button onClick={() => decrementQuantity(pr.productName)}>-</button></td>
                        <td><p>{quantities[pr.productName] || 0}</p></td>
                        <td><button onClick={() => incrementQuantity(pr.productName)}>+</button></td>                
                    </tr>
                    
                )}
            </tbody>
        </table>;
    return (
        <div className="indiv">
            <h1 id="tabelLabel">Catalog</h1>
            <h2 onClick={handleClick}>Cart</h2>
            <section>
                {showCart && <Cart products={prs} quants={quantities} />}
            </section>
            {contents}
        </div>
    );

    async function displayProducts() {
        const response = await fetch('product');
        const data = await response.json();
        setPrs(data);
    }
}

export default MainPage;