import './styles/Footer.css'

function Footer(){
    return(
        <footer>
            <section className="footer">
                <div className="footer-container">
                    <div className="footer-box">
                        <h3>Instagram</h3>
                        <a href="https://www.instagram.com/idc_anymore_bruv/">Mahammad Babayev</a>
                        <a href="https://www.instagram.com/murado_06/">Murad Orujov</a>
                    </div>

                    <div className="footer-box">
                        <h3>Contact Details</h3>
                        <a href="mailto: rasimbabayev9g19@gmail.com">rasimbabayev9g19@gmail.com</a>
                        <a href="mailto: morujov48@gmail.com">morujov48@gmail.com</a>
                    </div>
                </div>
            </section>

            <div className="copyright">
                <h4>
                    Copyright &#169; 2024 BlueSky Developers. All rights reserved.
                </h4>
            </div>
        </footer>
    )
}

export default Footer