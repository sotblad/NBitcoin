using NBitcoin;
using NBitcoin.DataEncoders;
using NBitcoin.Protocol;
using NBitcoin.RPC;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;

namespace NBitcoin.Altcoins
{
	public class MonetaryUnit : NetworkSetBase
	{
		public static MonetaryUnit Instance { get; } = new MonetaryUnit();

		public override string CryptoCode => "MUE";

		private MonetaryUnit()
		{

		}
		//Format visual studio
		//{({.*?}), (.*?)}
		//Tuple.Create(new byte[]$1, $2)
		static Tuple<byte[], int>[] pnSeed6_main = {
			Tuple.Create(new byte[]{0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0xff,0xff,0x33,0x0f,0xde,0xe0}, 19687),
			Tuple.Create(new byte[]{0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0xff,0xff,0x25,0x78,0xbe,0x4c}, 19687),
			Tuple.Create(new byte[]{0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0xff,0xff,0x25,0x78,0xba,0x55}, 19687),
			Tuple.Create(new byte[]{0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0xff,0xff,0xb9,0xca,0x8c,0x3c}, 19687),
			Tuple.Create(new byte[]{0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0xff,0xff,0xbc,0x47,0xdf,0xce}, 19687),
			Tuple.Create(new byte[]{0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0xff,0xff,0xb9,0xc2,0x8e,0x7a}, 19687),
		};
		static Tuple<byte[], int>[] pnSeed6_test = {
			Tuple.Create(new byte[]{0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0xff,0xff,0x58,0x44,0x34,0xac}, 19685),
			Tuple.Create(new byte[]{0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0xff,0xff,0x25,0x78,0xba,0x55}, 19685),
			Tuple.Create(new byte[]{0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0xff,0xff,0xbc,0x47,0xdf,0xce}, 19685),
			Tuple.Create(new byte[]{0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0xff,0xff,0xb9,0xc2,0x8e,0x7a}, 19685),
		};        		     		

#pragma warning disable CS0618 // Type or member is obsolete
		public class MonetaryUnitConsensusFactory : ConsensusFactory
		{
			private MonetaryUnitConsensusFactory()
			{
			}

			public static MonetaryUnitConsensusFactory Instance { get; } = new MonetaryUnitConsensusFactory();

			public override BlockHeader CreateBlockHeader()
			{
				return new MonetaryUnitBlockHeader();
			}
			public override Block CreateBlock()
			{
				return new MonetaryUnitBlock(new MonetaryUnitBlockHeader());
			}
		}

		public class MonetaryUnitBlockHeader : BlockHeader
		{
			public override uint256 GetPoWHash()
			{
                                throw new NotSupportedException("Not supported.");
			}
		}

		public class MonetaryUnitBlock : Block
		{
			public MonetaryUnitBlock(MonetaryUnitBlockHeader header) : base(header)
			{

			}
			public override ConsensusFactory GetConsensusFactory()
			{
				return MonetaryUnitConsensusFactory.Instance;
			}
		}

#pragma warning restore CS0618 // Type or member is obsolete

		protected override void PostInit()
		{
			RegisterDefaultCookiePath("MonetaryUnit");
		}

		protected override NetworkBuilder CreateMainnet()
		{
			NetworkBuilder builder = new NetworkBuilder();
			builder.SetConsensus(new Consensus()
			{
				SubsidyHalvingInterval = 210000, 
				MajorityEnforceBlockUpgrade = 750,
				MajorityRejectBlockOutdated = 950,
				MajorityWindow = 1000,
				BIP34Hash = new uint256("0b58ed450b3819ca54ab0054c4d220ca4f887d21c9e55d2a333173adf76d987f"), 
				PowLimit = new Target(new uint256("00000fffffffffffffffffffffffffffffffffffffffffffffffffffffffffff")),
				PowTargetTimespan = TimeSpan.FromSeconds(10 * 40),
				PowTargetSpacing = TimeSpan.FromSeconds(1 * 40),
				PowAllowMinDifficultyBlocks = false,
				PowNoRetargeting = false,
				RuleChangeActivationThreshold = 250,
				MinerConfirmationWindow = 1000,
				CoinbaseMaturity = 15,
				ConsensusFactory = MonetaryUnitConsensusFactory.Instance
			})
			.SetBase58Bytes(Base58Type.PUBKEY_ADDRESS, new byte[] { 16 })
			.SetBase58Bytes(Base58Type.SCRIPT_ADDRESS, new byte[] { 76 })
			.SetBase58Bytes(Base58Type.SECRET_KEY, new byte[] { 126 })
			.SetBase58Bytes(Base58Type.EXT_PUBLIC_KEY, new byte[] { 0x02, 0x2D, 0x25, 0x33 })
			.SetBase58Bytes(Base58Type.EXT_SECRET_KEY, new byte[] { 0x02, 0x21, 0x31, 0x2B })
			.SetBech32(Bech32Type.WITNESS_PUBKEY_ADDRESS, Encoders.Bech32("mue"))
			.SetBech32(Bech32Type.WITNESS_SCRIPT_ADDRESS, Encoders.Bech32("mue"))
			.SetMagic(0xEAFDC491)
			.SetPort(19687) 
			.SetRPCPort(19688)
			.SetMaxP2PVersion(80000)
			.SetName("mue-main")
			.AddAlias("mue-mainnet")
			.AddAlias("monetaryunit-mainnet")
			.AddAlias("monetaryunit-main")
			.AddDNSSeeds(new[]
			{
				new DNSSeedData("dns1.monetaryunit.org", "dns1.monetaryunit.org"),
				new DNSSeedData("dns2.monetaryunit.org", "dns2.monetaryunit.org"),
				new DNSSeedData("dns3.monetaryunit.org", "dns3.monetaryunit.org")
			})
			.AddSeeds(ToSeed(pnSeed6_main)) 
			.SetGenesis("010000000000000000000000000000000000000000000000000000000000000000000000c787795041016d5ee652e55e3a6aeff6c8019cf0c525887337e0b4206552691613f7fc58f0ff0f1ea12400000101000000010000000000000000000000000000000000000000000000000000000000000000ffffffff4004ffff001d010438506f77657264652062792042697473656e642d4575726f7065636f696e2d4469616d6f6e642d4d41432d42332032332f4170722f32303137ffffffff010000000000000000434104678afdb0fe5548271967f1a67130b7105cd6a828e03909a67962e0ea1f61deb649f6bc3f4cef38c4f35504e51ec112de5c384df7ba0b8d578a4c702b6bf11d5fac00000000");
			return builder;
		}

   		protected override NetworkBuilder CreateTestnet()
		{
			var builder = new NetworkBuilder();
			builder.SetConsensus(new Consensus()
			{
				SubsidyHalvingInterval = 210000,
				MajorityEnforceBlockUpgrade = 51,
				MajorityRejectBlockOutdated = 75,
				MajorityWindow = 1000,
				PowLimit = new Target(new uint256("00000fffffffffffffffffffffffffffffffffffffffffffffffffffffffffff")),
				PowTargetTimespan = TimeSpan.FromSeconds(1 * 60),
				PowTargetSpacing = TimeSpan.FromSeconds(1 * 10),
				PowAllowMinDifficultyBlocks = true,
				PowNoRetargeting = false,
				RuleChangeActivationThreshold = 1,
				MinerConfirmationWindow = 2,
				CoinbaseMaturity = 100,
				ConsensusFactory = MonetaryUnitConsensusFactory.Instance
			})
			.SetBase58Bytes(Base58Type.PUBKEY_ADDRESS, new byte[] { 139 })
			.SetBase58Bytes(Base58Type.SCRIPT_ADDRESS, new byte[] { 19 })
			.SetBase58Bytes(Base58Type.SECRET_KEY, new byte[] { 239 })
			.SetBase58Bytes(Base58Type.EXT_PUBLIC_KEY, new byte[] { 0x3A, 0x80, 0x61, 0xA1 })
			.SetBase58Bytes(Base58Type.EXT_SECRET_KEY, new byte[] { 0x3A, 0x80, 0x58, 0x37 })
			.SetBech32(Bech32Type.WITNESS_PUBKEY_ADDRESS, Encoders.Bech32("tmue"))
			.SetBech32(Bech32Type.WITNESS_SCRIPT_ADDRESS, Encoders.Bech32("tmue"))
			.SetMagic(0xBD657647)
			.SetPort(19685)
			.SetRPCPort(19686)
			.SetMaxP2PVersion(80000)
			.SetName("mue-test")
			.AddAlias("mue-testnet")
			.AddAlias("monetaryunit-test")
			.AddAlias("monetaryunit-testnet")
			.AddDNSSeeds(new[]
			{
				new DNSSeedData("testnetdns.monetaryunit.org", "testnetdns.monetaryunit.org")
			})
			.AddSeeds(ToSeed(pnSeed6_test))
			.SetGenesis("010000000000000000000000000000000000000000000000000000000000000000000000ff00e3481f61b255420602f7af626924221a41224b0d645bd2f082f82c8bc50a5746ff58f0ff0f1e98611a000101000000010000000000000000000000000000000000000000000000000000000000000000ffffffff4004ffff001d010438506f77657264652062792042697473656e642d4575726f7065636f696e2d4469616d6f6e642d4d41432d42332032332f4170722f32303137ffffffff01807c814a00000000434104678afdb0fe5548271967f1a67130b7105cd6a828e03909a67962e0ea1f61deb649f6bc3f4cef38c4f35504e51ec112de5c384df7ba0b8d578a4c702b6bf11d5fac00000000");
			return builder;
		}

		protected override NetworkBuilder CreateRegtest()
		{
			var builder = new NetworkBuilder();
			builder.SetConsensus(new Consensus()
			{
				SubsidyHalvingInterval = 150,
				MajorityEnforceBlockUpgrade = 51,
				MajorityRejectBlockOutdated = 75,
				MajorityWindow = 144,
				PowLimit = new Target(new uint256("7fffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffff")),
				PowTargetTimespan = TimeSpan.FromSeconds(24 * 60 * 60),
				PowTargetSpacing = TimeSpan.FromSeconds(1 * 60),
				PowAllowMinDifficultyBlocks = true,
				MinimumChainWork = uint256.Zero,
				PowNoRetargeting = true,
				RuleChangeActivationThreshold = 108,
				MinerConfirmationWindow = 144,
				CoinbaseMaturity = 100,
				ConsensusFactory = MonetaryUnitConsensusFactory.Instance
			})
			.SetBase58Bytes(Base58Type.PUBKEY_ADDRESS, new byte[] { 111 })
			.SetBase58Bytes(Base58Type.SCRIPT_ADDRESS, new byte[] { 196 })
			.SetBase58Bytes(Base58Type.SECRET_KEY, new byte[] { 239 })
			.SetBase58Bytes(Base58Type.EXT_PUBLIC_KEY, new byte[] { 0x04, 0x35, 0x87, 0xCF })
			.SetBase58Bytes(Base58Type.EXT_SECRET_KEY, new byte[] { 0x04, 0x35, 0x83, 0x94 })
			.SetBech32(Bech32Type.WITNESS_PUBKEY_ADDRESS, Encoders.Bech32("tmue")) 
			.SetBech32(Bech32Type.WITNESS_SCRIPT_ADDRESS, Encoders.Bech32("tmue")) 
			.SetMagic(0xad7ecfa2)
			.SetPort(19685)
			.SetRPCPort(19686)
			.SetMaxP2PVersion(80000)
			.SetName("mue-reg")
			.AddAlias("mue-regtest")
			.AddAlias("monetaryunit-reg")
			.AddAlias("monetaryunit-regtest")
			.SetGenesis("010000000000000000000000000000000000000000000000000000000000000000000000c787795041016d5ee652e55e3a6aeff6c8019cf0c525887337e0b4206552691613f7fc58f0ff0f1ea12400000101000000010000000000000000000000000000000000000000000000000000000000000000ffffffff4004ffff001d010438506f77657264652062792042697473656e642d4575726f7065636f696e2d4469616d6f6e642d4d41432d42332032332f4170722f32303137ffffffff010000000000000000434104678afdb0fe5548271967f1a67130b7105cd6a828e03909a67962e0ea1f61deb649f6bc3f4cef38c4f35504e51ec112de5c384df7ba0b8d578a4c702b6bf11d5fac00000000");
			return builder;
		}
	}
}
